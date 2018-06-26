using System;
using System.Collections;
using System.Reflection;

namespace ServicesAgreement.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool IsSimpleType(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsSimpleType(typeInfo.GetGenericArguments()[0]);
            }
            return typeInfo.IsPrimitive
              || typeInfo.IsEnum
              || type == typeof(string)
              || type == typeof(DateTime)
              || type == typeof(decimal);
        }

        public static bool IsCollection(this Type type)
        {
            return (type.GetInterface(nameof(IEnumerable)) != null);
        }

        public static bool IsGenericCollection(this Type type)
        {
            return type.IsGenericType && IsCollection(type);
        }

        public static bool IsDictionary(this Type type)
        {
            return (type.GetInterface(nameof(IDictionary)) != null);
        }
    }
}
