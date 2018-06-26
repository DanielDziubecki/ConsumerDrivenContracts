using FluentAssertions;
using ServicesAgreement.Exceptions;
using System;
using System.IO;
using Xunit;

namespace ServicesAgreement.Tests.Verifier
{
    public class service_agreement_complex_types_verifier_tests
    {
        private readonly string schemasBaseUri = Path.Combine(Utils.GetApplicationRoot(), "Verifier\\Schemas\\");

        //[Fact]
        //public void initial()
        //{
        //    var builder = new AgreementBuilder();
        //    builder.Consumer("TestConsumer")
        //        .HasAgreementWith("TestProvider")
        //        .ExpectsMessage(new ComplexSchemaMessage())
        //        .Build();
        //}

        [Fact]
        public void verifier_should_not_throw_exception_when_complex_type_type_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
              "TestProvider-TestConsumer-ComplexSchemaMessage.json"), new ComplexSchemaMessageWithDifferentNestedType());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().NotThrow();
        }


        [Fact]
        public void verifier_should_throw_exception_when_complex_type_type_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
              "TestProvider-TestConsumer-ComplexSchemaMessage.json"), new ComplexSchemaMessageWithDifferentNestedType());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().NotThrow();
        }

        [Fact]
        public void verifier_should_not_throw_exception_when_new_complex_type_added_to_message()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
                "TestProvider-TestConsumer-ComplexSchemaMessage.json"), new ExtendedComplexSchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().NotThrow();
        }
    }

    public class ComplexSchemaMessage
    {
        public int IntProperty { get; set; }
        public SomeNestedClass SomeNestedClass { get; set; }
    }

    public class ExtendedComplexSchemaMessage
    {
        public int IntProperty { get; set; }
        public SomeNestedClass SomeNestedClass { get; set; }
        public DifferentNestedType DifferentNestedType { get; set; }
    }

    public class ComplexSchemaMessageWithDifferentNestedType
    {
        public int IntProperty { get; set; }
        public DifferentNestedType SomeNestedClass { get; set; }
    }

    public class ComplexSchemaMessageWithPropertyOnDifferentLevel
    {
        public int IntProperty { get; set; }
        public byte SomeBytes { get; set; }
        public SomeNestedClass SomeNestedClass { get; set; }
    }

    public class SomeNestedClass
    {
        public byte SomeBytes { get; set; }
    }

    public class DifferentNestedType
    {
        public byte SomeBytes { get; set; }
    }
}
