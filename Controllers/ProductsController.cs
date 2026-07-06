using May2026ProductsBackendApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace May2026ProductsBackendApp.Controllers
{
    
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // Static in-memory data
        private static List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 75000
            },
            new Product
            {
                Id = 2,
                Name = "Mobile",
                Price = 25000
            },
            new Product
            {
                Id = 3,
                Name = "Keyboard",
                Price = 1500
            },
            new Product
            {
                Id = 4,
                Name = "Mouse",
                Price = 550
            }
        };


        // First user should be authenticated i.e he should login through OAuth2
        // i.e. he should be part of my Azure AD Tenant

        // Once authenticated he should have either BasicRole or AdminRole
        /// <summary>
        /// /////   step-2
        /// </summary>
        /// <returns></returns>
        [Authorize]             /////// Actually implemented Azure AD OAuth2
        [HttpGet("getproducts")]
        public IActionResult GetProducts()
        {
            // Read connection string from appsettings.json

            // This is hard coding
            //var connectionString = "Server=tcp:testappserver50.database.windows.net,1433;Initial Catalog=testappdb50;Persist Security Info=False;User ID=sqluser;Password=MyIndia@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // Lets fetch it from app-config settings file
            string connectionString = _configuration.GetConnectionString("mySQLAppConnectionString");

            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT Id, Name, Price FROM tblMstProducts";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"])
                            });
                        }
                    }
                }
            }

            return Ok(products);
        }


        // First user should be authenticated i.e he should login through OAuth2
        // i.e. he should be part of my Azure AD Tenant

        // Once authenticated, he should have AdminRole
        [Authorize]
        [HttpPost("addproduct")]
        public IActionResult AddProduct(Product product)
        {
            // so user is authenticated and he can reach this line

            // not every user of Azure AD should be able to access
            // only the user of Azure AD who has Admin access should be able to add a new product

            // we have to read principal id of user


            _products.Add(product);

            return Ok(new
            {
                Message = "Product added successfully",
                Product = product
            });
        }
    }
}
