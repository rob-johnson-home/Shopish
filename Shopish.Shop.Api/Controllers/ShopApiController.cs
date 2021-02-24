using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Shopish.Shop.Api.ViewModels;
using Shopish.Shop.Api.Service.Services;

namespace Shopish.Shop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopApiController : ControllerBase
    {
        private IShopService _shopService;

        private readonly ILogger<ShopApiController> _logger;

        public ShopApiController(ILogger<ShopApiController> logger,IShopService shopService )
        {
            _logger = logger;
            _shopService = shopService;
        }

        [HttpGet("get-featured-products", Name = "GetFeaturedProducts")]
        [Produces("application/json")]
        public async Task<ProductResultViewModel> GetFeaturedProducts()
        {
            try
            {
                var products = await _shopService.GetFeaturedProducts();
                var result = new ProductResultViewModel { message = "OK", products = products } ;
                return result;
            } catch (Exception e)
            {
                return new ProductResultViewModel { message = "ERROR", products = null};
            }
            
        }

        [HttpGet("get-categories", Name = "GetCategories")]
        [Produces("application/json")]
        public async Task<CategoryResultViewModel> GetCategories()
        {
            try
            {
                var categories = await _shopService.GetCategories();
                var result = new CategoryResultViewModel { message = "OK", categories = categories };
                return result;
            } catch (Exception e)
            {
                return new CategoryResultViewModel { message = "ERROR", categories = null };

            }
        }

        [HttpGet("get-products-by-category", Name = "GetProductsByCategory")]
        [Consumes("application/json"), Produces("application/json")]
        public async Task<ProductResultViewModel> GetProductsByCategory(string categoryId)
        {
            // my contract goes here
            if (string.IsNullOrEmpty(categoryId))
            {
                return new ProductResultViewModel { message = "Please provide a category ID", products = null };
            }
            //
            try
            {
                var products = await _shopService.GetProductByCategoryID(categoryId);
                var result = new ProductResultViewModel { message = "OK", products = products };
                return result;
            }
            catch (Exception e)
            {
                return new ProductResultViewModel { message = "ERROR", products = null };
            }
        }
    }
}
