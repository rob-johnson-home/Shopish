using Shopish.Shop.Api.EFDataAccess.Data;
using System.Collections.Generic;
using System.Linq;
using Shopish.Shop.Api.Domain.Model;

namespace Shopish.Shop.Api.EFDataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {


        public ProductRepository(ShopDbContext dbContext) : base(dbContext)
        {
        }

        public List<Product> GetFeaturedProducts()
        {
            var query = base.ExecuteSqlQuery(StoredProcedures.GetFeaturedProducts,System.Data.CommandType.StoredProcedure);
            return query.ToList();
        }

        public List<Product> GetProductsByCategoryID(string categoryID)
        {
            var query = base.ExecuteSqlQuery(StoredProcedures.GetProductsByCategory, System.Data.CommandType.StoredProcedure, 
                new Microsoft.Data.SqlClient.SqlParameter[] { new Microsoft.Data.SqlClient.SqlParameter("Category", categoryID )});
            return query.ToList();
        }
    }
}
