using System;
using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Model.RequiredFields
{
    public sealed class SimpleCollectionRequiredField : ISchemaRequiredField , IEquatable<SimpleCollectionRequiredField>
    {
        public string Name { get; }
        public int Level { get; }
        public string Type { get; }

        public SimpleCollectionRequiredField(string name, int level, string type)
        {
            Name = name;
            Level = level;
            Type = type;
        }

        public bool Equals(SimpleCollectionRequiredField other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Level == other.Level && Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is SimpleCollectionRequiredField field && Equals(field);
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
