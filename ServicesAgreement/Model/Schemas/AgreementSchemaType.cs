using System;

namespace ServicesAgreement.Model.Schemas
{
    public sealed class AgreementSchemaType
    {
        public AgreementSchemaType(string name, Type type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public Type Type { get; }
    }
}
