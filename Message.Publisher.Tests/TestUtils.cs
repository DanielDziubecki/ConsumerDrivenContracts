using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace Message.Publisher.Tests
{
    public static class TestUtils
    {
        public static IServiceCollection Replace<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime)
                      where TService : class
                      where TImplementation : class, TService
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));

            services.Remove(descriptorToRemove);

            var descriptorToAdd = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);

            services.Add(descriptorToAdd);

            return services;
        }

        public static IServiceCollection Replace<TService>(this IServiceCollection services,object instance, ServiceLifetime lifetime)
                    where TService : class
        {
            var descriptorToRemove = services.FirstOrDefault(d => d.ServiceType == typeof(TService));

            services.Remove(descriptorToRemove);

            var descriptorToAdd = new ServiceDescriptor(typeof(TService), instance.GetType(), lifetime);

            services.Add(descriptorToAdd);

            return services;
        }

        public static DbSet<T> CreateMockDbSet<T>(IEnumerable<T> data)
        where T : class
        {
            var dataQuery = data.AsQueryable();
            var mockSet = Substitute.For<DbSet<T>, IQueryable<T>>();

            ((IQueryable<T>)mockSet).Provider.Returns(dataQuery.Provider);
            ((IQueryable<T>)mockSet).Expression.Returns(dataQuery.Expression);
            ((IQueryable<T>)mockSet).ElementType.Returns(dataQuery.ElementType);
            ((IQueryable<T>)mockSet).GetEnumerator().Returns(data.GetEnumerator());

            return mockSet;
        }
    }
}
