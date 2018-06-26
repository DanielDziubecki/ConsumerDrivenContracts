using System;

namespace ServicesAgreement.Strategies
{
    public interface ITypeStrategyBase<out T>
    {
        T Get(Type collectionType);
    }
}