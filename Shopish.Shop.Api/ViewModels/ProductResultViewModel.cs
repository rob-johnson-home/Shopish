using Shopish.Shop.Api.Service.ViewModels;
using System.Collections.Generic;

namespace Shopish.Shop.Api.ViewModels
{
    public class ProductResultViewModel : BaseResultViewModel
    {
        public List<ProductViewModel> products { get; set; }
    }
}
