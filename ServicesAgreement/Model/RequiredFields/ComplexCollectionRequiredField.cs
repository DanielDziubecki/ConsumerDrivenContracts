using System;
using System.Collections.Generic;
using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Model.RequiredFields
{
    public sealed class ComplexCollectionRequiredField : ICollectionSchema, IEquatable<ComplexCollectionRequiredField>
    {
        public string Name { get; }
        public int Level { get; }
        public string Type { get; }
        public IEnumerable<ISchemaRequiredField> RequiredFields { get; }

        public ComplexCollectionRequiredField(string name, int level, string type, IEnumerable<ISchemaRequiredField> requiredFields)
        {
            Name = name;
            Level = level;
            Type = type;
            RequiredFields = requiredFields;
        }

        public bool Equals(ComplexCollectionRequiredField other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Level == other.Level;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ComplexCollectionRequiredField && Equals((ComplexCollectionRequiredField) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Level;
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RequiredFields != null ? RequiredFields.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

}
