using System;
using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Model.RequiredFields
{
    public sealed class SimpleRequiredField : ISchemaRequiredField, IEquatable<SimpleRequiredField>
    {
        public string Name { get; }
        public int Level { get; }
        public string Type { get; }

        public SimpleRequiredField(string name, int level, string type)
        {
            Name = name;
            Level = level;
            Type = type;
        }

        public bool Equals(SimpleRequiredField other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Level == other.Level && Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is SimpleRequiredField field && Equals(field);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Level;
            }
        }
    }
}
