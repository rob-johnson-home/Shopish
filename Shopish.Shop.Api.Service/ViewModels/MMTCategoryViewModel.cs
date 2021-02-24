using System;

namespace Shopish.Shop.Api.Service.ViewModels
{
    public class CategoryViewModel 
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string SKUPrefix { get; set; }
    }
}
