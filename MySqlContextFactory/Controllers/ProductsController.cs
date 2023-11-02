using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlContextFactory.Data.MyDatabase;
using MySqlContextFactory.Data.MyDatabase.Models;
using MySqlContextFactory.Services;

namespace MySqlContextFactory.Controllers
{
    /// <summary>
    /// This controller is using dbContext to serve the data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        readonly MyDatabaseDbContext dbContext;
        IGenericRepository<Customer> genericRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductsController(MyDatabaseDbContext dbContext, IGenericRepository<Customer> genericRepository)
        {
            this.dbContext = dbContext;
            this.genericRepository = genericRepository;
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
            var prods = await genericRepository.GetAllAsync();

            var products = await dbContext.Products.ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<Product?> GetProduct(int productID)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductID == productID);
            return product;
        }
    }
}