using System;
using ServicesAgreement.Factories;
using ServicesAgreement.Model.RequiredFields;
using Xunit;
using Xunit.Abstractions;

namespace ServicesAgreement.Tests.Schema
{
    public class agreement_schema_with_simple_types_tests
    {
        private readonly ITestOutputHelper helper;

        public agreement_schema_with_simple_types_tests(ITestOutputHelper helper)
        {
            this.helper = helper;
        }
        [Fact]
        public void schema_should_contains_int_field()
        {
            var msg = new SimpleTypesTestMessage
            {
                IntTestProperty = 100
            };
            var factory = new RequiredFieldFactory();
            var expectedRequiredField = new SimpleRequiredField(name: "IntTestProperty", level: 0, type: "System.Int32");

            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_string_field()
        {
            var msg = new SimpleTypesTestMessage
            {
                IntTestProperty = 100
            };
            var factory = new RequiredFieldFactory();
            var expectedRequiredField = new SimpleRequiredField(name: "StringProperty", level: 0, type: "System.String");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_bool_field()
        {
            var msg = new SimpleTypesTestMessage
            {
                BoolProperty = true
            };
            var factory = new RequiredFieldFactory();
            var expectedRequiredField = new SimpleRequiredField(name: "BoolProperty", level: 0, type: "System.Boolean");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_byte_field()
        {
            var msg = new SimpleTypesTestMessage
            {
                BoolProperty = true
            };
            var factory = new RequiredFieldFactory();
            var expectedRequiredField = new SimpleRequiredField(name: "ByteProperty", level: 0, type: "System.Byte");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }

        [Fact]
        public void schema_should_contains_datetime_field()
        {
            var msg = new SimpleTypesTestMessage
            {
                DateTimeProperty = DateTime.Now
            };
            var factory = new RequiredFieldFactory();
            var expectedRequiredField = new SimpleRequiredField(name: "DateTimeProperty", level: 0, type: "System.DateTime");
            factory.GetRequiredFields(msg.GetType()).ShouldContainEquivalentTo(expectedRequiredField, helper);
        }
    }

    public class SimpleTypesTestMessage
    {
        public int IntTestProperty { get; set; }
        public string StringProperty { get; set; }
        public bool BoolProperty { get; set; }
        public byte ByteProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
    }
}
