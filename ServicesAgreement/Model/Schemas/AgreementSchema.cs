using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ServicesAgreement.Services;

namespace ServicesAgreement.Model.Schemas
{
    public sealed class AgreementSchema : IEquatable<AgreementSchema>
    {

        public string Provider { get; }
        public string Consumer { get; }
        public string Description { get; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.None)]
        public object MetaData { get; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.None)]
        public object Message { get; }


        public IEnumerable<ISchemaRequiredField> RequiredFields { get; }

        public AgreementSchema(string provider,
            string consumer,
            string description,
            object metaData,
            object message,
            IEnumerable<ISchemaRequiredField> requiredFields)
        {
            RequiredFields = requiredFields;
            Provider = provider;
            Consumer = consumer;
            Description = description;
            MetaData = metaData;
            Message = message;
        }

        public bool Equals(AgreementSchema other)
        {
            InternalVerifier.Verify(this, other);
            return true;
        }
    }
}
