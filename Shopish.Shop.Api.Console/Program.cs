using MMTShop.Cli.Api;
using System;

namespace MMTShop.Cli

{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MMTShopApi();
            var categories = client.GetCategories();
            Console.WriteLine(categories);
            
        }
    }
}
