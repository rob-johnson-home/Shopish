using Shopish.Shop.Api.Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopish.Shop.Api.Service.Services
{
    public interface IShopService
    {
        // 3 things were requested in the doc :
        public Task<List<CategoryViewModel>> GetCategories();
        public Task<List<ProductViewModel>> GetFeaturedProducts();
        public Task<List<ProductViewModel>> GetProductByCategoryID(string category);
    }
}
