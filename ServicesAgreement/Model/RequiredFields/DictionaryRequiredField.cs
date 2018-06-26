using System;
using System.Collections.Generic;
using ServicesAgreement.Model.Schemas;

namespace ServicesAgreement.Model.RequiredFields
{
    public sealed class DictionaryRequiredField : IKeyValueSchema , IEquatable<DictionaryRequiredField>
    {
        public string Name { get; }
        public int Level { get; }
        public string Type { get; }
        public IEnumerable<ISchemaRequiredField> KeyFields { get; }
        public IEnumerable<ISchemaRequiredField> ValueFields { get; }

        public DictionaryRequiredField(string name, int level, string type, IEnumerable<ISchemaRequiredField> keyFields, IEnumerable<ISchemaRequiredField> valueFields)
        {
            Name = name;
            Level = level;
            Type = type;
            KeyFields = keyFields;
            ValueFields = valueFields;
        }

        public bool Equals(DictionaryRequiredField other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Level == other.Level;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is DictionaryRequiredField field && Equals(field);
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
