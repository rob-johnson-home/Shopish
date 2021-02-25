using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopish.Shop.Api.Domain.Model;
using Shopish.Shop.Api.EFDataAccess.Data;
using Shopish.Shop.Api.EFDataAccess.Repositories;
using Shopish.Shop.Api.Service.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMTShopApi.Test
{
    [TestClass]
    public class Shopish_Shop_Api_Product_Reppository_Test
    {

        // Adjust these to match your test data
        public int TESTCATEGORYCOUNT = 5;
        public int TESTFEATUREDPRODUCTSCOUNT = 0;
        private int TESTPRODUCTSBYCATEGORYCOUNT = 1;
        private IShopService _service;

        //

        [TestInitialize]
        public void TestInit()
        {

            var categoryJson = @"";


            var categoryList = new List<Category>
            {
                new Category {ID = new System.Guid() , Name = "Home",  SKUPrefix = "1" },
                new Category { ID = new System.Guid(), Name ="Garden", SKUPrefix ="2" },
                new Category {ID = new System.Guid(), Name = "Electronics", SKUPrefix ="3" },
                new Category { ID = new System.Guid(), Name ="Fitness",SKUPrefix = "4" },
                new Category { ID = new System.Guid(), Name ="Toys",SKUPrefix ="5" },
            };

            var productList = new List<Product>
            {
                new Product {ID = new System.Guid() , Name = "Test Home Product",  SKU = "10001", Price=5.99M, Description="Test Home Product Description", CategoryID = categoryList.Find(c => c.Name == "Home").ID },
                new Product {ID = new System.Guid() , Name = "Test Home Product",  SKU = "10001", Price=5.99M, Description="Test Home Product Description", CategoryID = categoryList.Find(c => c.Name == "Home").ID },
                new Product {ID = new System.Guid() , Name = "Test Home Product",  SKU = "10001", Price=5.99M, Description="Test Home Product Description", CategoryID = categoryList.Find(c => c.Name == "Home").ID },
                new Product {ID = new System.Guid() , Name = "Test Home Product",  SKU = "10001", Price=5.99M, Description="Test Home Product Description", CategoryID = categoryList.Find(c => c.Name == "Home").ID },
                new Product {ID = new System.Guid() , Name = "Test Home Product",  SKU = "10001", Price=5.99M, Description="Test Home Product Description", CategoryID = categoryList.Find(c => c.Name == "Home").ID }
            };


            ('10001', 'Test Home Product', 'Test Home Product Description', 5.99, (select ID from dbo.mmtcategories where name = 'Home')),
	('20001', 'Test Garden Product', 'Test Garden Product Description', 7.56, (select ID from dbo.mmtcategories where name = 'Garden')),
	('30001', 'Test Electronics Product', 'Test Electronics Product Description', 199.99, (select ID from dbo.mmtcategories where name = 'Electronics')),
	('40001', 'Test Fitness Product', 'Test Fitness Product Description', 1200.00, (select ID from dbo.mmtcategories where name = 'Fitness')),
	('50001', 'Test Toys Product', 'Test Toys Product Description', 29.99, (select ID from dbo.mmtcategories where name = 'Toys'))


            var productRepo = new Mock<IProductRepository>();//ProductRepository(context.Object);
            var categoryRepo = new Mock<ICategoryRepository>();// (context.Object);

            productRepo.Setup(p => p.GetFeaturedProducts()).Returns(new List<Product>());
            productRepo.Setup(p => p.GetProductsByCategoryID(It.IsAny<string>())).Returns(new List<Product>());
            categoryRepo.Setup(p => p.GetCategories()).Returns(new List<Category>());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Shopish.Shop.Api.Service.ViewModels.AutoMappings());
            });

            AutoMapper.IMapper mapper = config.CreateMapper();

            _service = new ShopService(mapper, productRepo.Object, categoryRepo.Object);
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
