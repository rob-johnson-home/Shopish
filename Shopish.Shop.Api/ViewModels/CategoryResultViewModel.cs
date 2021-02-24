using Shopish.Shop.Api.Service.ViewModels;
using System.Collections.Generic;

namespace Shopish.Shop.Api.ViewModels
{
    public class CategoryResultViewModel : BaseResultViewModel
    {
        public List<CategoryViewModel> categories { get; set; }
    }
}
