using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopish.Shop.Api.Domain.Model
{
    [Table("MMTProducts")]
    public class Product : EntityBase
    {
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required,Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [ForeignKey("Category")]
        public Guid CategoryID { get; set; }
        public Category Category { get; set; }
        //public string FeatureRange { get; set; }
    }
}
