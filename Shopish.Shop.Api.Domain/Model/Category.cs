using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopish.Shop.Api.Domain.Model
{
    [Table("MMTCategories")]
    public class Category : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SKUPrefix { get; set; }
    }
}
