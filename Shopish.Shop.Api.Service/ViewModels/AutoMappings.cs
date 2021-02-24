using AutoMapper;
using Shopish.Shop.Api.Domain.Model;

namespace Shopish.Shop.Api.Service.ViewModels
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel,Product >();

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
        }
    }
}
