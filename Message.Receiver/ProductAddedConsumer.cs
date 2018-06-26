using MassTransit;
using Message.Contracts;
using System;
using System.Threading.Tasks;

namespace Message.Receiver
{
    public class ProductAddedConsumer : IConsumer<ProductAdded>
    {
        public Task Consume(ConsumeContext<ProductAdded> product)
        {
            Console.WriteLine($"Product added {product.Message.ProductId}");
            return Task.CompletedTask;
        }
    }
}
