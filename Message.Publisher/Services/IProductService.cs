using System.Threading.Tasks;
using Message.Publisher.DTO;

namespace Message.Publisher.Services
{
    public interface IProductService
    {
        Task AddProduct(ProductDto product);
    }
}
