using System;
using System.Collections.Generic;
using ServicesAgreement.Consts;
using ServicesAgreement.Factories;
using ServicesAgreement.Model.RequiredFields;
using Xunit;
using Xunit.Abstractions;

namespace ServicesAgreement.Tests.Schema
{
    public class agreement_schema_with_collections_tests
    {
        private readonly ITestOutputHelper helper;

        public agreement_schema_with_collections_tests(ITestOutputHelper helper)
        {
            this.helper = helper;
        }

        [Fact]
        public void schema_should_contains_collection_of_simple_types()
        {
            var msg = new SimpleCollectionTypes
            {

            };

            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new SimpleCollectionRequiredField(name: "CollectionOfInts", level: 0, type: TypesNamesConsts.CollectionOf + "System.Int32");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_collection_of_simple_types_on_level_one()
        {
            var msg = new TestMessageClass
            {
            };

            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new SimpleCollectionRequiredField(name: "CollectionOfBytes", level: 1, type: TypesNamesConsts.CollectionOf + "System.Byte");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_collection_of_complex_types()
        {
            var msg = new NestedComplexCollectionTypes
            {
                CollectionOfComplexTypes = new ComplexTypeInCollection[]
                {

                }
            };

            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new ComplexCollectionRequiredField(name: "CollectionOfComplexTypes",
                level: 0,
                type: TypesNamesConsts.CollectionOf + "ComplexTypeInCollection",
                requiredFields: factory.GetRequiredFields(typeof(ComplexTypeInCollection)));
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_collection_of_complex_types_with_nested_complex_types()
        {
            var msg = new NestedComplexCollectionTypeWithNestedComplexType
            {
            };

            var factory = new RequiredFieldFactory();
    
            var expectedRequiredField = new ComplexCollectionRequiredField(name: "CollectionOfComplexTypes",
                level: 0,
                type: TypesNamesConsts.CollectionOf + "ComplexTypeInCollectionWithNestedComplexType",
                requiredFields: factory.GetRequiredFields(typeof(ComplexTypeInCollectionWithNestedComplexType)));

            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }
    

        [Fact]
        public void schema_should_contains_collection_of_complex_types_with_nested_collection_of_types()
        {
            var msg = new ComplexTypeInCollectionWithNestedCollection
            {
            };

            var factory = new RequiredFieldFactory();
            var expectedRequiredField = new ComplexCollectionRequiredField(name: "CollectionWithNestedColletion",
                level: 0,
                type: TypesNamesConsts.CollectionOf + "TypeWithNestedColletion",
                requiredFields: factory.GetRequiredFields(typeof(TypeWithNestedColletion)));

            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_support_generic_collections_of_simple_types()
        {
            var msg = new TypeWithComplexTypeWithGenericCollectionOfSimpleTypes
            {
            };

            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new SimpleCollectionRequiredField(name: "GenericOfSimples",
                level: 1,
                type: TypesNamesConsts.CollectionOf + "System.Int32"
                );

            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_support_generic_collections_of_complex_types()
        {
            var msg = new TypeWithComplexTypeWithGenericCollectionOfComplexTypes
            {
            };

            var factory = new RequiredFieldFactory();

            var expectedRequiredField = new ComplexCollectionRequiredField(
                name: "GenericOfComplex",
                level: 1,
                type: TypesNamesConsts.CollectionOf + "NestedComplexType",
                    requiredFields: factory.GetRequiredFields(typeof(NestedComplexType))
            );

            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }


        public class SimpleCollectionTypes
        {
            public int IntProperty { get; set; }
            public int[] CollectionOfInts { get; set; }
        }

        public class NestedSimpleCollectionTypes
        {
            public DateTime DattTimeProperty { get; set; }
            public byte[] CollectionOfBytes { get; set; }
        }

        public class NestedComplexCollectionTypes
        {
            public ComplexTypeInCollection[] CollectionOfComplexTypes { get; set; }
        }

        public class NestedComplexCollectionTypeWithNestedComplexType
        {
            public ComplexTypeInCollectionWithNestedComplexType[] CollectionOfComplexTypes { get; set; }
        }

        public class ComplexTypeInCollection
        {
            public int IntProperty { get; set; }
            public bool BoolProperty { get; set; }
        }

        public class ComplexTypeInCollectionWithNestedComplexType
        {
            public int IntProperty { get; set; }
            public bool BoolProperty { get; set; }
            public NestedComplexType NestedComplexType { get; set; }
        }

        public class NestedComplexType
        {
            public int IntProperty { get; set; }
            public DateTime DateTimeProperty { get; set; }
        }

        public class TestMessageClass
        {
            public NestedSimpleCollectionTypes NestedSimpleCollection { get; set; }
        }

        public class ComplexTypeInCollectionWithNestedCollection
        {
            public TypeWithNestedColletion[] CollectionWithNestedColletion { get; set; }
        }

        public class TypeWithNestedColletion
        {
            public int IntProperty { get; set; }
            public NestedComplexType[] NestedComplexTypes { get; set; }
        }

        public class TypeWithComplexTypeWithGenericCollectionOfSimpleTypes
        {
            public TypeWithGenericCollectionOfSimples TypeWithGenericCollectionOfSimples { get; set; }
        }

        public class TypeWithComplexTypeWithGenericCollectionOfComplexTypes
        {
            public TypeWithGenericCollectionOfComplexs TypeWithGenericCollectionOfComplexs { get; set; }
        }

        public class TypeWithGenericCollectionOfSimples
        {
            public List<int> GenericOfSimples { get; set; }
        }

        public class TypeWithGenericCollectionOfComplexs
        {
            public List<NestedComplexType> GenericOfComplex { get; set; }
        }
    }
}
