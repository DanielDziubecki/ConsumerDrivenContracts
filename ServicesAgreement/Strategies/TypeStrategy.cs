using System;
using ServicesAgreement.Extensions;

namespace ServicesAgreement.Strategies
{
    public class TypeStrategy<T> : ITypeStrategyBase<T>
    {
        public TypeStrategy(Func<Type, T> simpleTypeAction, Func<Type, T> collectionTypeAction, Func<Type, T> complexTypeAction)
        {
            SimpleTypeAction = simpleTypeAction;
            CollectionTypeAction = collectionTypeAction;
            ComplexTypeAction = complexTypeAction;
        }

        public Func<Type, T> SimpleTypeAction { get; }
        public Func<Type, T> CollectionTypeAction { get; }
        public Func<Type, T> ComplexTypeAction { get; }

        public T Get(Type collectionType)
        {
            if (collectionType.IsSimpleType())
                return SimpleTypeAction(collectionType);
    
            return collectionType.IsCollection() ? CollectionTypeAction(collectionType) : ComplexTypeAction(collectionType);
        }
    }
}
