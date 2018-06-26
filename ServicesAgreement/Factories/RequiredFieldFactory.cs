using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ServicesAgreement.Consts;
using ServicesAgreement.Extensions;
using ServicesAgreement.Model.RequiredFields;
using ServicesAgreement.Model.Schemas;
using ServicesAgreement.Strategies;
using ServicesAgreement.Utils;

namespace ServicesAgreement.Factories
{
    public sealed class RequiredFieldFactory
    {
        private readonly Stack<KeyValuePair<int, AgreementSchemaType>> complexTypeStack = new Stack<KeyValuePair<int, AgreementSchemaType>>();
        private readonly TypeStrategy<IEnumerable<ISchemaRequiredField>> dictionaryBase;
        private readonly TypeStrategy<string> dictionaryRootNameBase;

        public RequiredFieldFactory()
        {
            dictionaryBase = new TypeStrategy<IEnumerable<ISchemaRequiredField>>(
            simpleTypeAction: type => new ISchemaRequiredField[] { new SimpleRequiredField(TypesNamesConsts.DictionaryKeyName, 0, type.FullName) },
            collectionTypeAction: type =>
            {
                var elementTypes = CollectionElementTypeProvider.Get(type);
                if (elementTypes.Length > 1)
                    throw new NotSupportedException("Dictionary of dictionary not supported");

                return GetRequiredFields(elementTypes.First());
            },
            complexTypeAction: GetRequiredFields);

            dictionaryRootNameBase = new TypeStrategy<string>(
                simpleTypeAction: type => type.ToString(),
                collectionTypeAction: type =>
                {
                    return TypesNamesConsts.CollectionOf + new CollectionStrategy<string>(
                                   dictionaryAction: type1 => throw new NotSupportedException("Dictionary of dictionary not supported"),
                                   simpleNonGenericAction: type1 => CollectionElementTypeProvider.Get(type1)[0].Name,
                                   complexNonGenericAction: type1 => CollectionElementTypeProvider.Get(type1)[0].Name).Get(type);
                },
                complexTypeAction: type => type.Name);

        }

        public IEnumerable<ISchemaRequiredField> GetRequiredFields(Type messageType)
        {
            var schemaRequiredFields = new List<ISchemaRequiredField>();
            const int level = 0;

            complexTypeStack.Push(new KeyValuePair<int, AgreementSchemaType>(level, new AgreementSchemaType(TypesNamesConsts.InitialType, messageType)));

            while (complexTypeStack.Any())
            {
                var complexType = complexTypeStack.Pop();
                foreach (var property in complexType.Value.Type.GetProperties().OrderByDescending(x => x.PropertyType.IsSimpleType()))
                {
                    schemaRequiredFields.Add(GetField(property, complexType));
                }
            }
            return schemaRequiredFields;
        }

        private ISchemaRequiredField GetField(PropertyInfo child, KeyValuePair<int, AgreementSchemaType> parent)
        {
            var fieldStrategy = new TypeStrategy<ISchemaRequiredField>(
                simpleTypeAction: type => new SimpleRequiredField(name: child.Name,
                                                                  level: parent.Key,
                                                                  type: child.PropertyType.ToString()),
                collectionTypeAction: type => GetCollectionField(child, parent),
                complexTypeAction: type =>
                {
                    var nextLevel = parent.Key + 1;
                    complexTypeStack.Push(new KeyValuePair<int, AgreementSchemaType>(nextLevel, new AgreementSchemaType(child.PropertyType.Name, child.PropertyType)));

                    return new ClassRequiredField(name: child.Name,
                                                  level: parent.Key,
                                                  type: parent.Value.Name == TypesNamesConsts.InitialType ?
                                                                             child.PropertyType.Name :
                                                                             parent.Value.Name);
                });

            return fieldStrategy.Get(child.PropertyType);
        }

        private ISchemaRequiredField GetCollectionField(PropertyInfo child, KeyValuePair<int, AgreementSchemaType> parent)
        {

            var collectionStrategy = new CollectionStrategy<ISchemaRequiredField>(
                dictionaryAction: collectionType =>
                {
                    var keyValueTypes = CollectionElementTypeProvider.Get(collectionType);
                    var keyType = keyValueTypes[0];
                    var keySchema = dictionaryBase.Get(keyType);
                    var valueType = keyValueTypes[1];
                    var valueSchema = dictionaryBase.Get(valueType);

                    return new DictionaryRequiredField(name: child.Name,
                        level: parent.Key,
                        type: string.Format(TypesNamesConsts.DictionaryOf,
                            dictionaryRootNameBase.Get(keyType),
                            dictionaryRootNameBase.Get(valueType)),
                        keyFields: keySchema,
                        valueFields: valueSchema);
                },
                simpleNonGenericAction: collectionType =>
                {
                    var elementType = CollectionElementTypeProvider.Get(collectionType)[0];
                    return new ComplexCollectionRequiredField(
                        name: child.Name,
                        level: parent.Key,
                        type: TypesNamesConsts.CollectionOf + elementType.Name,
                        requiredFields: GetRequiredFields(elementType));
                },
                complexNonGenericAction: collectionType =>
                {
                    var elementType = CollectionElementTypeProvider.Get(collectionType)[0];
                    return new SimpleCollectionRequiredField(
                        name: child.Name,
                        level: parent.Key,
                        type: TypesNamesConsts.CollectionOf + elementType.FullName);
                });

            return collectionStrategy.Get(child.PropertyType);
        }
    }
}
