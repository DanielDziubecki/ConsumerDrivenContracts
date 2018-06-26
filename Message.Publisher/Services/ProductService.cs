using System;
using System.Threading.Tasks;
using MassTransit;
using Message.Contracts;
using Message.Publisher.DB;
using Message.Publisher.DTO;

namespace Message.Publisher.Services
{
    public class ProductService : IProductService
    {
        private readonly IBus bus;
        private readonly ProductContext context;

        public ProductService(IBus bus, ProductContext context)
        {
            this.bus = bus;
            this.context = context;
        }

        public async Task AddProduct(ProductDto productDto)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var product = new Product { Name = productDto.Name };
                    context.Products.Add(product);
                    context.SaveChanges();
                    await bus.Publish(new ProductAdded
                    {
                        ProductId = product.Id,
                        Msgs = new System.Collections.Generic.List<TestMsg> {
                            new TestMsg {Type = 12, Type1 = "123"} },
                        TypeNotExistsingInContract = new TypeNotExistsingInContract { DateTimeProperty = DateTime.Now }
                    });
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

        }
    }
}
