namespace WebAPIServer.Modules.Catalog.DataAccesses.Data.Seeders
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            CategorySeedData.Initialize(serviceProvider);
            ProductSeedData.Initialize(serviceProvider);
            /*ComboSeedData.Initialize(serviceProvider);
            ComboProductSeedData.Initialize(serviceProvider);*/
        }
    }
}
