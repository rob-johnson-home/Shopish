using Shopish.Shop.Api.EFDataAccess.Data;
using Shopish.Shop.Api.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Shopish.Shop.Api.EFDataAccess.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        public CategoryRepository(ShopDbContext dbContext) : base (dbContext)
        {
        }

        public List<Category> GetCategories()
        {
                var query = base.ExecuteSqlQuery(StoredProcedures.GetCategories, System.Data.CommandType.StoredProcedure);
                return query.ToList();
        }
    }
}
