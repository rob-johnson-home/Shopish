
using System;

namespace Shopish.Shop.Api.Service.ViewModels
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public Guid CategoryID { get; set; }
        public CategoryViewModel Category { get; set; }
        //public string FeatureRange { get; set; }
    }
}
