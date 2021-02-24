
using Microsoft.EntityFrameworkCore;
using Shopish.Shop.Api.Domain.Model;

namespace Shopish.Shop.Api.EFDataAccess.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
        {
        }
        // dbset for all entities
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
