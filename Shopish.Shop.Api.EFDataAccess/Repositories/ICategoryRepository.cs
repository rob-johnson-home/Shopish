using Shopish.Shop.Api.Domain.Model;
using System.Collections.Generic;

namespace Shopish.Shop.Api.EFDataAccess.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetCategories();
    }
}
