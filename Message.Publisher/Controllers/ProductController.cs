using Message.Publisher.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Message.Publisher.Services;

namespace Message.Publisher.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public async Task AddProduct([FromBody]ProductDto productDto)
        {
            await productService.AddProduct(productDto);
        }
    }
}
