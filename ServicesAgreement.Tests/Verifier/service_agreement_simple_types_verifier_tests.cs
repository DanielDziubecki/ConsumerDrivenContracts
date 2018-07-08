using FluentAssertions;
using ServicesAgreement.Exceptions;
using System;
using System.IO;
using Xunit;

namespace ServicesAgreement.Tests.Verifier
{
    public class service_agreement_simple_types_verifier_tests
    {
        private readonly string schemasBaseUri = Path.Combine(Utils.GetApplicationRoot(), "Verifier\\Schemas\\");

        [Fact]
        public void initial()
        {
            var builder = new AgreementBuilder();
            builder.Consumer("TestConsumer")
                .HasAgreementWith("TestProvider")
                .ExpectsMessage(new SimpleSchemaMessage())
                .Build();
        }

        [Fact]
        public void verifier_should_throw_exception_when_simple_type_type_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
                "TestProvider-TestConsumer-SimpleSchemaMessage.json"), new SimpleSchemaMessageWithWrongType());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage(string.Format(ExceptionMessages.ProviderSchemaBreaksAgreement, "{\"Name\":\"DatetimeProperty\",\"Level\":0,\"Type\":\"System.DateTime\"}"));

        }

        [Fact]
        public void verifier_should_throw_exception_when_simple_type_name_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
                "TestProvider-TestConsumer-SimpleSchemaMessage.json"), new SimpleSchemaMessageWithWrongTypeName());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage(string.Format(ExceptionMessages.ProviderSchemaBreaksAgreement, "{\"Name\":\"DatetimeProperty\",\"Level\":0,\"Type\":\"System.DateTime\"}"));

        }

        [Fact]
        public void verifier_should_not_throw_exception_when_new_simple_type_added_to_message()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
                "TestProvider-TestConsumer-SimpleSchemaMessage.json"), new ExtendedSimpleSchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().NotThrow();
        }

        public class SimpleSchemaMessage
        {
            public int IntProperty { get; set; }
            public DateTime DatetimeProperty { get; set; }
        }

        public class SimpleSchemaMessageWithWrongType
        {
            public int IntProperty { get; set; }
            public string DatetimeProperty { get; set; }
        }

        public class SimpleSchemaMessageWithWrongTypeName
        {
            public int IntProperty { get; set; }
            public DateTime WrongTypeName { get; set; }
        }

        public class ExtendedSimpleSchemaMessage
        {
            public int IntProperty { get; set; }
            public DateTime DatetimeProperty { get; set; }
            public byte ByteProperty { get; set; }
        }
    }
}
