using System;
using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Model.RequiredFields
{
    public sealed class ClassRequiredField : ISchemaRequiredField, IEquatable<ClassRequiredField>
    {
        public string Name { get; }
        public int Level { get; }
        public string Type { get; }

        public ClassRequiredField(string name, int level, string type)
        {
            Name = name;
            Level = level;
            Type = type;
        }

        public bool Equals(ClassRequiredField other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Level == other.Level;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ClassRequiredField field && Equals(field);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Level;
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

}
