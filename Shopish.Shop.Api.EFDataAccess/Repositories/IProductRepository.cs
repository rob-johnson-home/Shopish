using Shopish.Shop.Api.Domain.Model;
using System.Collections.Generic;

namespace Shopish.Shop.Api.EFDataAccess.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByCategoryID(string categoryID);
        List<Product> GetFeaturedProducts();
    }
}
