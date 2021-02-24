namespace Shopish.Shop.Api.EFDataAccess.Repositories
{
    //List of stored procedures
    public class StoredProcedures
    {
        
        // TODO - should really pop these in to a configureation file
        public static string GetCategories = "dbo.spMMTGetCategories";
        public static string GetFeaturedProducts = "dbo.spMMTGetFeaturedProducts";
        public static string GetProductsByCategory = "dbo.spMMTGetProductsByCategory @Category";    }
}
