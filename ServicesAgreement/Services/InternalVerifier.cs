using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ServicesAgreement.Exceptions;
using ServicesAgreement.Extensions;
using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Services
{
    internal static class InternalVerifier
    {
        public static void Verify(AgreementSchema providerSchema, AgreementSchema consumerSchema)
        {
            BaseVerification(providerSchema,consumerSchema);
            Verify(providerSchema.RequiredFields, consumerSchema.RequiredFields);
        }

        public static void Verify(IEnumerable<ISchemaRequiredField> providerRequiredFields, IEnumerable<ISchemaRequiredField> consumerRequiredFields)
        {
            var providerGroups = providerRequiredFields.GroupBy(x => x.Level).ToList();
            var consumerGroups = consumerRequiredFields.GroupBy(x => x.Level).ToList();

            foreach (var consumerGroup in consumerGroups)
            {
                var providerGroup = providerGroups.Where(x => x.Key == consumerGroup.Key).ToList();
                foreach (var requiredField in consumerGroup)
                {
                    var levelProviderRequiredFields = providerGroup.SelectMany(x => x);
                    levelProviderRequiredFields.ShouldContainRequiredField(requiredField);
                    if (requiredField.IsCollectionRequiredField())
                    {
                        var consumerCollection = (ICollectionSchema)requiredField;
                        levelProviderRequiredFields.ShouldContainRequiredField(consumerCollection);
                        var providerCollection = (ICollectionSchema)levelProviderRequiredFields.Single(x => x.Equals(consumerCollection));
                        Verify(providerCollection.RequiredFields, consumerCollection.RequiredFields);
                    }
                    if (requiredField.IsKeyValueRequiredField())
                    {
                        var consumerCollection = (IKeyValueSchema)requiredField;
                        levelProviderRequiredFields.ShouldContainRequiredField(consumerCollection);
                        var providerCollection = (IKeyValueSchema)levelProviderRequiredFields.Single(x => x.Equals(consumerCollection));
                        Verify(providerCollection.KeyFields, consumerCollection.KeyFields);
                        Verify(providerCollection.ValueFields, consumerCollection.ValueFields);
                    }
                }
            }
        }

        public static void BaseVerification(AgreementSchema providerSchema, AgreementSchema consumerSchema)
        {
            if (!consumerSchema.Provider.Equals(providerSchema.Provider))
            {
                throw new AgreementViolationException(string.Format(ExceptionMessages.InvalidProviderName, consumerSchema.Provider, providerSchema.Provider));
            }

            if (!consumerSchema.Consumer.Equals(providerSchema.Consumer))
            {
                throw new AgreementViolationException(string.Format(ExceptionMessages.InvalidConsumerName, consumerSchema.Consumer, providerSchema.Consumer));
            }

            var providerMetadata = JsonConvert.SerializeObject(providerSchema.MetaData);
            var consumerMetadata = JsonConvert.SerializeObject(consumerSchema.MetaData);

            if (!string.Equals(providerMetadata, consumerMetadata))
            {
                throw new AgreementViolationException(string.Format(ExceptionMessages.InvalidMetadata, consumerMetadata, providerMetadata));
            }
        }
         
    }
}
