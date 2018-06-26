using System;
using ServicesAgreement.Factories;
using ServicesAgreement.Model.RequiredFields;
using Xunit;
using Xunit.Abstractions;

namespace ServicesAgreement.Tests.Schema
{
    public class agreement_schema_with_complex_types_tests
    {
        private readonly ITestOutputHelper helper;

        public agreement_schema_with_complex_types_tests(ITestOutputHelper helper)
        {
            this.helper = helper;
        }

        [Fact]
        public void schema_should_contains_complex_type()
        {
            var factory = new RequiredFieldFactory();
            var msg = new SimpleWithComplexTypeTestMessage
            {
                FirstNestedComplexType = new FirstNestedComplexType
                {
                    FirstBoolProperty = true,
                    FirstStringProperty = "NestedString"
                }
            };

            var expectedRequiredField = new ClassRequiredField(name: "FirstNestedComplexType", level: 0, type: "FirstNestedComplexType");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_simple_type_in_complex_type_on_level_one()
        {
            var factory = new RequiredFieldFactory();
            var msg = new SimpleWithComplexTypeTestMessage
            {
                FirstNestedComplexType = new FirstNestedComplexType()
            };

            var expectedRequiredField = new SimpleRequiredField(name: "FirstBoolProperty", level: 1, type: "System.Boolean");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_simple_type_on_level_one_when_complex_type_exists()
        {
            var factory = new RequiredFieldFactory();
            var msg = new SimpleWithComplexTypeTestMessage
            {
                FirstNestedComplexType = new FirstNestedComplexType()
            };

            var expectedRequiredField = new SimpleRequiredField(name: "StringProperty", level: 0, type: "System.String");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_simple_type_on_level_two()
        {
            var factory = new RequiredFieldFactory();
            var msg = new SimpleWithComplexTypeTestMessage
            {
                FirstNestedComplexType = new FirstNestedComplexType
                {
                    ThirdNestedComplexType = new ThirdNestedComplexType()
                }
            };

            var expectedRequiredField = new SimpleRequiredField(name: "ThirdNestedDateTimeProperty", level: 2, type: "System.DateTime");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        public class SimpleWithComplexTypeTestMessage
        {
            public string StringProperty { get; set; }
            public bool BoolProperty { get; set; }
            public FirstNestedComplexType FirstNestedComplexType { get; set; }
            public SecondNestedComplexType SecondNestedComplexType { get; set; }
        }

        public class FirstNestedComplexType
        {
            public string FirstStringProperty { get; set; }
            public bool FirstBoolProperty { get; set; }
            public ThirdNestedComplexType ThirdNestedComplexType { get; set; }
        }

        public class SecondNestedComplexType
        {
            public string SecondNestedStringProperty { get; set; }
            public bool SecondNestedBoolProperty { get; set; }
        }

        public class ThirdNestedComplexType
        {
            public DateTime ThirdNestedDateTimeProperty { get; set; }
        }
    }
}
