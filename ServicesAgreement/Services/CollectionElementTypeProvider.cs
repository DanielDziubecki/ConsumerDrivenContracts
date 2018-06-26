using ServicesAgreement.Extensions;
using System;

namespace ServicesAgreement.Utils
{
    public static class CollectionElementTypeProvider
    {
        public static Type[] Get(Type type)
        {
            if (type.IsDictionary())
            {
                var keyValue = type.GetGenericArguments();
                return new[] { keyValue[0], keyValue[1] };
            }

            if (type.IsGenericCollection())
                return new[] { type.GetGenericArguments()[0] };

            return new[] {type.GetElementType()};
        }
    }
}