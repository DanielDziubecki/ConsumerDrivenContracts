using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ServicesAgreement.Consts;
using ServicesAgreement.Factories;
using ServicesAgreement.Model.RequiredFields;
using ServicesAgreement.Model.Schemas;
using Xunit;
using Xunit.Abstractions;

namespace ServicesAgreement.Tests.Schema
{
    public class agreement_schema_with_key_value_types
    {
        private readonly ITestOutputHelper helper;

        public agreement_schema_with_key_value_types(ITestOutputHelper helper)
        {
            this.helper = helper;
        }

        [Fact]
        public void schema_should_support_dictionaries_with_simple_key_and_simple_value()
        {
            var msg = new TypeWithDictionaryWithSimpleKeyValue();
            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new DictionaryRequiredField(
                name: "SimpleSimpleDictionary",
                level: 0,
                type: string.Format(TypesNamesConsts.DictionaryOf, "System.Int32", "System.Int32"),
                keyFields: new ISchemaRequiredField[]{ new SimpleRequiredField(TypesNamesConsts.DictionaryKeyName, 0, "System.Int32") }, 
                valueFields: new ISchemaRequiredField[] { new SimpleRequiredField(TypesNamesConsts.DictionaryKeyName, 0, "System.Int32") }
            );

            var result = factory.GetRequiredFields(msg.GetType());
            result.ShouldContainEquivalentTo(expectedRequiredField, helper);
            ((DictionaryRequiredField)result.Single()).KeyFields.Should().BeEquivalentTo(expectedRequiredField.KeyFields);
            ((DictionaryRequiredField)result.Single()).ValueFields.Should().BeEquivalentTo(expectedRequiredField.ValueFields);
        }

        [Fact]
        public void schema_should_support_dictionaries_with_simple_key_and_complex_value()
        {
            var msg = new TypeWithDictionaryWithSimpleKeyComplexValue();
            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new DictionaryRequiredField(
                name: "SimpleComplexDictionary",
                level: 0,
                type: string.Format(TypesNamesConsts.DictionaryOf, "System.Int32", "ValueDictionaryType"),
                keyFields: new ISchemaRequiredField[] { new SimpleRequiredField(TypesNamesConsts.DictionaryKeyName, 0, "System.Int32") },
                valueFields: factory.GetRequiredFields(typeof(ValueDictionaryType))
            );

            var result = factory.GetRequiredFields(msg.GetType());
            result.ShouldContainEquivalentTo(expectedRequiredField, helper);
            ((DictionaryRequiredField)result.Single()).KeyFields.Should().BeEquivalentTo(expectedRequiredField.KeyFields);
            ((DictionaryRequiredField)result.Single()).ValueFields.Should().BeEquivalentTo(expectedRequiredField.ValueFields);
        }

        [Fact]
        public void schema_should_support_dictionaries_with_simple_key_and_collection_value()
        {
            var msg = new TypeWithDictionaryWithSimpleKeyCollectionValue();
            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new DictionaryRequiredField(
                name: "SimpleKeyCollectionValue",
                level: 0,
                type: string.Format(TypesNamesConsts.DictionaryOf, "System.Int32", TypesNamesConsts.CollectionOf+ "ValueDictionaryType"),
                keyFields: new ISchemaRequiredField[] { new SimpleRequiredField(TypesNamesConsts.DictionaryKeyName,0, "System.Int32") },
                valueFields: factory.GetRequiredFields(typeof(ValueDictionaryType))
            );

            var result = factory.GetRequiredFields(msg.GetType());
            result.ShouldContainEquivalentTo(expectedRequiredField, helper);
            ((DictionaryRequiredField)result.Single()).KeyFields.Should().BeEquivalentTo(expectedRequiredField.KeyFields);
            ((DictionaryRequiredField)result.Single()).ValueFields.Should().BeEquivalentTo(expectedRequiredField.ValueFields);
        }

        [Fact]
        public void schema_should_support_generic_collections_of_key_value_pair()
        {
            var msg = new NestedKeyValuePairCollection { };
            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new ComplexCollectionRequiredField(
                name: "TypeWithNestedColletionKeyValue",
                level: 0,
                type: TypesNamesConsts.CollectionOf + "KeyValuePair`2",
                requiredFields: factory.GetRequiredFields(typeof(KeyValuePair<string, ValueDictionaryType>))
            );

            var result = factory.GetRequiredFields(msg.GetType());
            result.ShouldContainEquivalentTo(expectedRequiredField, helper);
            ((ComplexCollectionRequiredField)result.Single()).RequiredFields.Should().BeEquivalentTo(expectedRequiredField.RequiredFields);
        }

        [Fact]
        public void schema_should_support_dictionaries_with_simple_key_and_key_value_pair_value()
        {
            var msg = new TypeWithDictionaryWithSimpleKeyKeyValuePairValue { };
            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new DictionaryRequiredField(
                name: "SimpleKeyCollectionValue",
                level: 0,
                type: string.Format(TypesNamesConsts.DictionaryOf, "System.Int32", "KeyValuePair`2"),
                keyFields: new ISchemaRequiredField[] { new SimpleRequiredField(TypesNamesConsts.DictionaryKeyName, 0, "System.Int32") },
                valueFields: factory.GetRequiredFields(typeof(KeyValuePair<DateTime, ValueDictionaryType>))
            );
            var result = factory.GetRequiredFields(msg.GetType());
                result.ShouldContainEquivalentTo(expectedRequiredField, helper);
            ((DictionaryRequiredField)result.Single()).KeyFields.Should().BeEquivalentTo(expectedRequiredField.KeyFields);
            ((DictionaryRequiredField)result.Single()).ValueFields.Should().BeEquivalentTo(expectedRequiredField.ValueFields);
        }

        public class NestedKeyValuePairCollection
        {
            public List<KeyValuePair<string, ValueDictionaryType>> TypeWithNestedColletionKeyValue { get; set; }
        }

        public class TypeWithDictionaryWithSimpleKeyValue
        {
            public Dictionary<int,int> SimpleSimpleDictionary { get; set; }
        }

        public class TypeWithDictionaryWithSimpleKeyComplexValue
        {
            public Dictionary<int, ValueDictionaryType> SimpleComplexDictionary { get; set; }
        }

        public class TypeWithDictionaryWithSimpleKeyCollectionValue
        {
            public Dictionary<int, List<ValueDictionaryType>> SimpleKeyCollectionValue { get; set; }
        }

        public class TypeWithDictionaryWithSimpleKeyKeyValuePairValue
        {
            public Dictionary<int, KeyValuePair<DateTime,ValueDictionaryType>> SimpleKeyCollectionValue { get; set; }
        }

        public class ValueDictionaryType
        {
            public int IntProperty { get; set; }
            public DateTime DateTimeProperty { get; set; }
            public NestedDictionaryType NestedDictionaryType { get; set; }
        }

        public class NestedDictionaryType
        {
            public DateTime DateTimeProperty { get; set; }
        }
    }
}
