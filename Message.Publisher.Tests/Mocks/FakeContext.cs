using Message.Publisher.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.IO;

namespace Message.Publisher.Tests.Mocks
{
    public class FakeContext : ProductContext
    {
        public FakeContext()
        {

        }

        public FakeContext(DbContextOptions<ProductContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public override DatabaseFacade Database => new FakeFacade(this);

        public override int SaveChanges()
        {
            return 1;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.Test.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

    public class FakeFacade : DatabaseFacade
    {
        public FakeFacade(DbContext context) : base(context)
        {
        }

        public override IDbContextTransaction BeginTransaction()
        {
            return Substitute.For<IDbContextTransaction>();
        }
    }
}
