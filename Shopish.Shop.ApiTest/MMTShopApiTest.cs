using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMTShopApi.Domain.Data;
using MMTShopApi.Repositories;
using MMTShopApi.Services;
using System.Threading.Tasks;

namespace MMTShopApi.Test
{
    [TestClass]
    public class MMTShopApiTest
    {
        private MMTShopService _service;

        // Adjust these to match your test data
        public int TESTCATEGORYCOUNT = 5;
        public int TESTFEATUREDPRODUCTSCOUNT = 3;
        private int TESTPRODUCTSBYCATEGORYCOUNT = 1;
        //

        [TestInitialize]
        public void TestInit()
        {

            // DONT Use the Db here, mock something up

            var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var Configuration = builder.Build();

            DbContextOptionsBuilder<MMTShopDbContext> optionsBuilder = new DbContextOptionsBuilder<MMTShopDbContext>();
            var x = optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MMTShop")).Options;

            MMTShopDbContext context = new MMTShopDbContext(x);
            var prodRepo = new MMTProductRepository(context);
            var catRepo = new MMTCategoryRepository(context);
            _service = new MMTShopService(prodRepo, catRepo);
        }


        /// <summary>
        /// Simple test to get a list of categories from a test install
        /// </summary>
        [TestMethod]
        public async Task TestCategories1()
        {
            var categoryList = await _service.GetCategories();
            Assert.IsTrue(categoryList.Count == TESTCATEGORYCOUNT);
        }

        /// <summary>
        /// Simple test to get a list of featured products from a test install
        /// </summary>
        [TestMethod]
        public async Task TestFeaturedProducts()
        {
            var featuredProducts = await _service.GetFeaturedProducts();
            Assert.IsTrue(featuredProducts.Count == TESTFEATUREDPRODUCTSCOUNT);
        }

        /// <summary>
        /// Simple test to get a list of products by category from a test install
        /// </summary>
        [TestMethod]
        public async Task TestProductsByCategory()
        {
            var categoryList = await _service.GetCategories();
            Assert.IsTrue(categoryList.Count > 0);
            var products = await _service.GetProductByCategoryID(categoryList[0].ID.ToString());
            Assert.IsTrue(products.Count == TESTPRODUCTSBYCATEGORYCOUNT);
        }
    }
}
