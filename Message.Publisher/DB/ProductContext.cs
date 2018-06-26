using Microsoft.EntityFrameworkCore;

namespace Message.Publisher.DB
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext()
        {

        }

        public ProductContext(DbContextOptions<ProductContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id); 
        }
    }
}
