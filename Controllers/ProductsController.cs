using May2026ProductsBackendApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace May2026ProductsBackendApp.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
            
        // Static in-memory data
        private static List<Product> _products = new List<Product>
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
            },
            new Product
            {
                ProductId = 4,
                ProductName = "Mouse",
                ProductPrice = 550
            }
        };


        // First user should be authenticated i.e he should login through OAuth2
        // i.e. he should be part of my Azure AD Tenant

        // Once authenticated he should have either BasicRole or AdminRole
        [HttpGet("getproducts")]
        public IActionResult GetProducts()
        {
            return Ok(_products);
        }


        // First user should be authenticated i.e he should login through OAuth2
        // i.e. he should be part of my Azure AD Tenant

        // Once authenticated, he should have AdminRole
        [HttpPost("addproduct")]
        public IActionResult AddProduct(Product product)
        {
            _products.Add(product);

            return Ok(new
            {
                Message = "Product added successfully",
                Product = product
            });
        }
    }
}
