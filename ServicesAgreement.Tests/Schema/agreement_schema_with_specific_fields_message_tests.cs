using FluentAssertions;
using ServicesAgreement.Consts;
using ServicesAgreement.Factories;
using ServicesAgreement.Model.RequiredFields;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ServicesAgreement.Tests.Schema
{
    public class agreement_schema_with_specific_fields_message_tests
    {
        [Fact]
        public void schema_should_support_simple_types_specific_fields()
        {
            var message = new SomeMessage();
            var factory = new RequiredFieldFactory();
            var fields = factory.GetRequiredFields(new { message.DateProperty, message.IntProperty }.GetType());

            fields.Should().HaveCount(2);
            fields.Should().Contain(new SimpleRequiredField("IntProperty", 0, "System.Int32"));
            fields.Should().Contain(new SimpleRequiredField("DateProperty", 0, "System.DateTime"));
        }

        [Fact]
        public void schema_should_support_complex_types_specific_fields()
        {
            var message = new SomeMessage();
            var factory = new RequiredFieldFactory();
            var fields = factory.GetRequiredFields(new { message.SomeClass }.GetType());

            fields.Should().HaveCount(3);
            fields.Should().Contain(new ClassRequiredField("SomeClass", 0, "SomeClass"));
        }

        [Fact]
        public void schema_should_support_simple_collections_specific_fields()
        {
            var message = new SomeMessage();
            var factory = new RequiredFieldFactory();
            var fields = factory.GetRequiredFields(new { message.DatesProperty }.GetType());

            fields.Should().HaveCount(1);
            fields.Should().Contain(new SimpleCollectionRequiredField("DatesProperty", 0, TypesNamesConsts.CollectionOf + "System.DateTime"));
        }

        public class SomeMessage
        {
            public int IntProperty { get; set; }
            public DateTime DateProperty { get; set; }
            public DateTime[] DatesProperty { get; set; }
            public SomeClass SomeClass { get; set; }
        }

        public class SomeClass
        {
            public List<int> SomeInts { get; set; }
            public string StringProperty { get; set; }
        }
    }
}
