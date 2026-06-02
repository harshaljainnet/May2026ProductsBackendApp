using May2026ProductsBackendApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace May2026ProductsBackendApp.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("getproducts")]
        public IActionResult GetProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    ProductName = "Laptop",
                    ProductPrice = 75000
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Mobile",
                    ProductPrice = 25000
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Keyboard",
                    ProductPrice = 1500
                }
            };

            return Ok(products);
        }
    }
}
