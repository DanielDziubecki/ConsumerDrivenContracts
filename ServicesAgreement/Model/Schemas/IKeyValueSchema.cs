using System.Collections.Generic;

namespace ServicesAgreement.Model.Schemas
{
    public interface IKeyValueSchema : ISchemaRequiredField
    {
        IEnumerable<ISchemaRequiredField> KeyFields { get; }
        IEnumerable<ISchemaRequiredField> ValueFields { get; }
    }
}
