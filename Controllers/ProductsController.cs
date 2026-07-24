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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IConfiguration configuration, ILogger<ProductsController> logger)
        {
            _configuration = configuration;
            _logger = logger;
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
        //[Authorize]             /////// Actually implemented Azure AD OAuth2
        [HttpGet("getproducts")]
        public IActionResult GetProducts()
        {
            // Read connection string from appsettings.json


            // custom code to validate token


            // This is hard coding
            //var connectionString = "Server=tcp:testappserver50.database.windows.net,1433;Initial Catalog=testappdb50;Persist Security Info=False;User ID=sqluser;Password=MyIndia@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // Lets fetch it from app-config settings file
            //string connectionString = _configuration.GetConnectionString("mySQLAppConnectionString");

            //List<Product> products = new List<Product>();

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

            //    string sql = "SELECT Id, Name, Price FROM tblMstProducts";

            //    using (SqlCommand command = new SqlCommand(sql, connection))
            //    {
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                products.Add(new Product
            //                {
            //                    Id = Convert.ToInt32(reader["Id"]),
            //                    Name = reader["Name"].ToString(),
            //                    Price = Convert.ToDecimal(reader["Price"])
            //                });
            //            }
            //        }
            //    }
            //}

            //return Ok(products);

            // write custom log in Azure Application Insights
            _logger.LogError("Actually error has not occured, but we are just testing");


            // Typical secure app - OAuth2, NSGs, VNET, App Gateway, Key vault, WAF


            // exception details
            //var notAnInteger = "ABCD";
            //var number = Convert.ToInt32(notAnInteger);


            // performance issues
            //await Task.Delay(10000); // Wait for 10 seconds
            // 90% of times its related to fetching data from db
            // async
            // connection pools
            // option = optimize the code
            // use cache memory e.g. redis cache
            // data archive


            // create simple CI-CD pipeline
            // Azure DevOps - free
            // new feature to implement
            // user stories
            // sprints
            // GitHUb -> PR -> Code review -> Test cases review -> Test cases covergae

            // Dev to Test -> Solution Architect
            // Test to Staging -> Solution Architect + Project Manager
            // Stagng to prod -> Delivey Manager
            // Azure DevOps
            // Thread.Sleep(10000); // Wait for 10 seconds


            // Explain me common metrics/alerts/dashboards you used to monitor resources
            // dashboards

            return Ok(_products);

        }


        // First user should be authenticated i.e he should login through OAuth2
        // i.e. he should be part of my Azure AD Tenant

        // Once authenticated, he should have AdminRole
        //[Authorize]
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
