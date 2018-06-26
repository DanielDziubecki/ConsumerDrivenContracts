using System;
using System.IO;
using FluentAssertions;
using ServicesAgreement.Exceptions;
using Xunit;

namespace ServicesAgreement.Tests.Verifier
{
    public class service_agreement_base_verifier_tests
    {
        private readonly string schemasBaseUri = Path.Combine(Utils.GetApplicationRoot(), "Verifier\\Schemas\\");

        //[Fact]
        //public void initial()
        //{
        //    var builder = new AgreementBuilder();
        //    builder.Consumer("TestConsumer")
        //        .HasAgreementWith("TestProvider")
        //        .ExpectsMessage(new BaseSchemaMessage())
        //        .ExpectsMetaData(new { messageNamespace = typeof(BaseSchemaMessage).FullName })
        //        .Build();
        //}

        [Fact]
        public void verifier_should_throw_exception_on_different_provider_name()
        {
            var verifier = new AgreementVerifer()
                .Provider("BreakProvider")
                .HasAgreementWith("TestConsumer")
                .WithMessage(new BaseSchemaMessage())
                .WithMetaData(new { messageNamespace = typeof(BaseSchemaMessage).FullName })
                .WithAgreementDestination(Path.Combine(schemasBaseUri,
                    "TestProvider-TestConsumer-BaseSchemaMessage.json"));

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage(string.Format(ExceptionMessages.InvalidProviderName, "TestProvider", "BreakProvider"));

        }

        [Fact]
        public void verifier_should_throw_exception_on_different_consumer_name()
        {
            var verifier = new AgreementVerifer()
                .Provider("TestProvider")
                .HasAgreementWith("BreakConsumer")
                .WithMessage(new BaseSchemaMessage())
                .WithMetaData(new { messageNamespace = typeof(BaseSchemaMessage).FullName })
                .WithAgreementDestination(Path.Combine(schemasBaseUri,
                    "TestProvider-TestConsumer-BaseSchemaMessage.json"));

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage(string.Format(ExceptionMessages.InvalidConsumerName, "TestConsumer", "BreakConsumer"));

        }

        [Fact]
        public void verifier_should_throw_exception_on_different_meta_data()
        {
            var verifier = new AgreementVerifer()
                .Provider("TestProvider")
                .HasAgreementWith("TestConsumer")
                .WithMessage(new BaseSchemaMessage())
                .WithMetaData(new { messageNamespace = typeof(int).Name })
                .WithAgreementDestination(Path.Combine(schemasBaseUri,
                    "TestProvider-TestConsumer-BaseSchemaMessage.json"));

            var expectedMetadata = "{\"messageNamespace\":\"ServicesAgreement.Tests.Verifier.BaseSchemaMessage\"}";
            var actualMetadata = "{\"messageNamespace\":\"Int32\"}";

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage(string.Format(ExceptionMessages.InvalidMetadata, expectedMetadata, actualMetadata));

        }
    }

    public class BaseSchemaMessage
    {
        public int IntProperty { get; set; }
        public DateTime DatetimeProperty { get; set; }
    }
}
