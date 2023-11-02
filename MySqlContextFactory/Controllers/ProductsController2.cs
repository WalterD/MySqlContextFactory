using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlContextFactory.Data.MyDatabase;
using MySqlContextFactory.Data.MyDatabase.Models;

namespace MySqlContextFactory.Controllers
{
    /// <summary>
    /// This controller is using dbContextFactory to serve the data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController2 : ControllerBase
    {
        readonly IDbContextFactory<MyDatabaseDbContext> dbContextFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductsController2(IDbContextFactory<MyDatabaseDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            return Ok($"{this.GetType().Name} :: {DateTime.Now}");
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<List<Product>> GetProducts()
        {
            using var db = dbContextFactory.CreateDbContext();
            var products = await db.Products.ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<Product?> GetProduct(int productID)
        {
            using var db = dbContextFactory.CreateDbContext();
            var product = await db.Products.FirstOrDefaultAsync(x => x.ProductID == productID);
            return product;
        }
    }
}