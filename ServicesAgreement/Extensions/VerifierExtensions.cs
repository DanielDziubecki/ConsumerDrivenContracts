using Newtonsoft.Json;
using ServicesAgreement.Exceptions;
using ServicesAgreement.Model.Schemas;
using System.Collections.Generic;
using System.Linq;

namespace ServicesAgreement.Extensions
{
    public static class VerifierExtensions
    {
        public static void ShouldContainRequiredField<ISchemaRequiredField>(this IEnumerable<ISchemaRequiredField> source, ISchemaRequiredField schemaRequiredField)
        {
            if (!source.Contains(schemaRequiredField))
            {
                throw new AgreementViolationException(string.Format(ExceptionMessages.ProviderSchemaBreaksAgreement, JsonConvert.SerializeObject(schemaRequiredField)));
            }
        }

        public static bool IsCollectionRequiredField(this ISchemaRequiredField schemaRequiredField)
        {
            return schemaRequiredField is ICollectionSchema;
        }

        public static bool IsKeyValueRequiredField(this ISchemaRequiredField schemaRequiredField)
        {
            return schemaRequiredField is IKeyValueSchema;
        }
    }
}
