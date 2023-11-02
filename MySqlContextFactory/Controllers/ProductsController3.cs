using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlContextFactory.Data.MyDatabase;
using MySqlContextFactory.Data.MyDatabase.Models;

namespace MySqlContextFactory.Controllers
{
    /// <summary>
    /// This controller is using dbContextFactory to serve the data. DbContextFactory is injected in each action that requires it.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController3 : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            return Ok($"{this.GetType().Name} :: {DateTime.Now}");
        }

        /// <summary>
        /// Injecting dbContextFactory. The "[FromServices]" part is not necessary starting wit .net core 7.
        /// </summary>
        [HttpGet]
        [Route("GetProducts")]
        public async Task<List<Product>> GetProducts(IDbContextFactory<MyDatabaseDbContext> dbContextFactory)
        {
            using var db = dbContextFactory.CreateDbContext();
            var products = await db.Products.ToListAsync();
            return products;
        }

        /// <summary>
        /// Use "[FromServices]" in .net core 6 and lower.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProduct")]
        public async Task<Product?> GetProduct([FromServices] IDbContextFactory<MyDatabaseDbContext> dbContextFactory, int productID)
        {
            using var db = dbContextFactory.CreateDbContext();
            var product = await db.Products.FirstOrDefaultAsync(x => x.ProductID == productID);
            return product;
        }
    }
}