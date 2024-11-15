namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Data.Seeders
{
    public static class SeedData
    {
        public static void Initialize(this IServiceProvider serviceProvider)
        {
            DirectorSeedData.Initialize(serviceProvider);
            GenreSeedData.Initialize(serviceProvider);
            SeatTypeSeedData.Initialize(serviceProvider); 
            CastMemberSeedData.Initialize(serviceProvider);
            HallSeedData.Initialize(serviceProvider);
            MovieSeedData.Initialize(serviceProvider);
            ShowSeedData.Initialize(serviceProvider);
        }
    }
}