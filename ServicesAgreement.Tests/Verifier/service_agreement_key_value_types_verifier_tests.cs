using FluentAssertions;
using ServicesAgreement.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ServicesAgreement.Tests.Verifier
{
    public class service_agreement_key_value_types_verifier_tests
    {
        private readonly string schemasBaseUri = Path.Combine(Utils.GetApplicationRoot(), "Verifier\\Schemas\\");

        [Fact]
        public void initial()
        {
            var builder = new AgreementBuilder();
            builder.Consumer("TestConsumer")
               .HasAgreementWith("TestProvider")
               .ExpectsMessage(new DictionarySchemaMessage())
               .Build();
        }

        [Fact]
        public void verifier_should_throw_exception_when_key_value_key_type_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
            "TestProvider-TestConsumer-KeyValueSchemaMessage.json"), new DifferentKeyKeyValueSchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage("Provider schema breaks agreement because {\"Name\":\"Key\",\"Level\":0,\"Type\":\"System.Int64\"} required field was not found");
        }

        [Fact]
        public void verifier_should_throw_exception_when_key_value_value_type_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
          "TestProvider-TestConsumer-KeyValueSchemaMessage.json"), new DifferentValueKeyValueSchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage("Provider schema breaks agreement because {\"Name\":\"Key\",\"Level\":0,\"Type\":\"System.Int64\"} required field was not found");
        }

        [Fact]
        public void verifier_should_throw_exception_when_dictionary_key_type_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
        "TestProvider-TestConsumer-DictionarySchemaMessage.json"), new DifferentKeyDictionarySchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage("Provider schema breaks agreement because {\"Name\":\"DictionaryKey\",\"Level\":0,\"Type\":\"System.Int64\"} required field was not found");
        }

        [Fact]
        public void verifier_should_throw_exception_when_dictionary_value_type_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
       "TestProvider-TestConsumer-DictionarySchemaMessage.json"), new DifferentValueDictionarySchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage("Provider schema breaks agreement because {\"Name\":\"DictionaryKey\",\"Level\":0,\"Type\":\"System.Int64\"} required field was not found");
        }
    }

    public class KeyValueSchemaMessage
    {
        public DateTime DateTimeProperty { get; set; }
        public List<KeyValuePair<long, DateTime>> KeyValueList { get; set; }
    }

    public class DictionarySchemaMessage
    {
        public DateTime DateTimeProperty { get; set; }
        public Dictionary<long, SomeClass> SomeDictionary { get; set; }
    }

    public class DifferentKeyDictionarySchemaMessage
    {
        public DateTime DateTimeProperty { get; set; }
        public Dictionary<DateTime, SomeClass> SomeDictionary { get; set; }
    }

    public class DifferentValueDictionarySchemaMessage
    {
        public DateTime DateTimeProperty { get; set; }
        public Dictionary<DateTime, byte> SomeDictionary { get; set; }
    }

    public class DifferentKeyKeyValueSchemaMessage
    {
        public DateTime DateTimeProperty { get; set; }
        public List<KeyValuePair<SomeClass, DateTime>> KeyValueList { get; set; }
    }

    public class DifferentValueKeyValueSchemaMessage
    {
        public DateTime DateTimeProperty { get; set; }
        public List<KeyValuePair<SomeClass, DateTime>> KeyValueList { get; set; }
    }

    public class SomeClass
    {
        public DateTime DateTimeProperty { get; set; }
    }
}
