using AutoMapper;
using Shopish.Shop.Api.Domain.Model;
using Shopish.Shop.Api.EFDataAccess.Repositories;
using Shopish.Shop.Api.Service.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopish.Shop.Api.Service.Services
{
    public class ShopService : IShopService
    {

        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public ShopService(IMapper mapper,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        /// get full list of categories
        /// 
        public async Task<List<CategoryViewModel>> GetCategories() {
            var categories = _categoryRepository.GetCategories();

            return _mapper.Map<List<CategoryViewModel>>(categories.ToList());
        }

        /// <summary>
        /// get currently featured products
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetFeaturedProducts() {
            var products = _productRepository.GetFeaturedProducts();

            return ProductLoadCategories(products);
        }

        /// <summary>
        /// get products by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public async Task<List<ProductViewModel>> GetProductByCategoryID(string categoryID) {


            var products = _productRepository.GetProductsByCategoryID(categoryID);

            return ProductLoadCategories(products);
        }

        //materialize categories inside products (as I dont see how to do this with SPs!)
        private List<ProductViewModel> ProductLoadCategories(IEnumerable<Product> products)
        {
            var categories = _categoryRepository.List();
            return products
            .Select(p => new ProductViewModel
            {
                ID = p.ID,
                SKU = p.SKU,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryID = p.CategoryID,
                Category = _mapper.Map<CategoryViewModel>(categories.First(categories => categories.ID == p.CategoryID))
            }).ToList();
        }
    }
}
