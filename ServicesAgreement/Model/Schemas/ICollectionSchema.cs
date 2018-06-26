using System.Collections.Generic;

namespace ServicesAgreement.Model.Schemas
{
    public interface ICollectionSchema : ISchemaRequiredField
    {
        IEnumerable<ISchemaRequiredField> RequiredFields { get; }
    }
}
