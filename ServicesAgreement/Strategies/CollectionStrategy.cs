using System;
using System.Linq;
using ServicesAgreement.Extensions;

namespace ServicesAgreement.Strategies
{
    public class CollectionStrategy<T> : ITypeStrategyBase<T>
    {
        public Func<Type, T> DictionaryAction { get; }
        public Func<Type, T> SimpleNonGenericAction { get; }
        public Func<Type, T> ComplexNonGenericAction { get; }


        public CollectionStrategy(Func<Type, T> dictionaryAction, Func<Type, T> simpleNonGenericAction, Func<Type, T> complexNonGenericAction)
        {
            DictionaryAction = dictionaryAction;
            SimpleNonGenericAction = simpleNonGenericAction;
            ComplexNonGenericAction = complexNonGenericAction;
        }

        public T Get(Type collectionType)
        {
            var collectionElementType = collectionType.GetElementType();
            if (collectionType.IsGenericCollection())
            {
                if (collectionType.IsDictionary())
                    return DictionaryAction(collectionType);

                collectionElementType = collectionType.GetGenericArguments().Single();
            }
           
            return !collectionElementType.IsSimpleType() ? SimpleNonGenericAction(collectionType) : ComplexNonGenericAction(collectionType);
        }
    }
}