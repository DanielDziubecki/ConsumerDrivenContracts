using FluentAssertions;
using ServicesAgreement.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace ServicesAgreement.Tests.Verifier
{
    public class service_agreement_collections_verifier_tests
    {
        private readonly string schemasBaseUri = Path.Combine(Utils.GetApplicationRoot(), "Verifier\\Schemas\\");

        [Fact]
        public void initial()
        {
            var builder = new AgreementBuilder();
            builder.Consumer("TestConsumer")
                .HasAgreementWith("TestProvider")
                .ExpectsMessage(new SimpleCollectiomSchemaMessage())
                .Build();
        }

        [Fact]
        public void verifier_should_throw_exception_when_collection_element_schema_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
             "TestProvider-TestConsumer-CollectionSchemaMessage.json"), new BrokenElementCollectionSchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>().WithMessage("Provider schema breaks agreement because {\"Name\":\"DateTimeProperty\",\"Level\":0,\"Type\":\"System.DateTime\"} required field was not found");
        }

        [Fact]
        public void verifier_should_not_throw_exception_when_collection_complex_element_name_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
          "TestProvider-TestConsumer-CollectionSchemaMessage.json"), new DifferentNameCollectionSchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().NotThrow();
        }

        [Fact]
        public void verifier_should_throw_exception_when_collection_simple_element_name_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
          "TestProvider-TestConsumer-SimpleCollectiomSchemaMessage.json"), new DifferentSimpleCollectiomSchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().Throw<AgreementViolationException>();
        }

        [Fact]
        public void verifier_should_not_throw_exception_when_collection_type_changes()
        {
            var verifier = Utils.GetVerifier(Path.Combine(schemasBaseUri,
           "TestProvider-TestConsumer-CollectionSchemaMessage.json"), new DifferentCollectionTypeSchemaMessage());

            Action verifyAction = () => { verifier.Verify(); };
            verifyAction.Should().NotThrow();
        }
    }


    public class CollectionSchemaMessage
    {
        public int IntProperty { get; set; }
        public List<CollectionElement> SomeCollection { get; set; }
    }

    public class SimpleCollectiomSchemaMessage
    {
        public long LongProperty { get; set; }
        public List<long> LongSomeCollection { get; set; }
    }

    public class DifferentSimpleCollectiomSchemaMessage
    {
        public long LongProperty { get; set; }
        public List<int> LongSomeCollection { get; set; }
    }

    public class DifferentCollectionTypeSchemaMessage
    {
        public int IntProperty { get; set; }
        public CollectionElement[] SomeCollection { get; set; }
    }

    public class BrokenElementCollectionSchemaMessage
    {
        public int IntProperty { get; set; }
        public List<BrokenCollectionElement> SomeCollection { get; set; }
    }

    public class DifferentNameCollectionSchemaMessage
    {
        public int IntProperty { get; set; }
        public List<DifferentNameCollectionElement> SomeCollection { get; set; }
    }

    public class CollectionElement
    {
        public int IntProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
    }

    public class DifferentNameCollectionElement
    {
        public int IntProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
    }

    public class BrokenCollectionElement
    {
        public int IntProperty { get; set; }
    }
}
