using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProductApp.Models;
using ProductApp.Services;

namespace ProductApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public ProductController(IProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAll();
            var currency = _configuration["DefaultCurrency"];

            var response = new ProductResponse { Currency = currency, Products = products };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _productService.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
    }
}
