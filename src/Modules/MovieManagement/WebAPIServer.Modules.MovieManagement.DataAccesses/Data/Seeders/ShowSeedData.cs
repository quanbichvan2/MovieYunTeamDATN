using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Data.Seeders
{
	internal static class ShowSeedData
	{
		public static void Initialize(this IServiceProvider serviceProvider)
		{
			using (var context = new MovieManagementDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieManagementDbContext>>()))
			{
				//if (!context.Shows.Any())
				//{
				//	context.Shows.AddRange(
				//		new Show
				//		{
				//			Id = Guid.Parse("84B0D047-C109-4D0A-9E2E-0CEAFC88AA65"),
				//			CinemaHallId = Guid.Parse("7A8D4DEA-15DD-4CDD-AD10-0AE010106891"),
				//			MovieId = Guid.Parse("8C1D9E76-419B-4CA1-844D-B9AC9207E284"),
				//			StartTime = DateTimeOffset.ParseExact("22/10/2024 14:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("22/10/2024 17:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://th.bing.com/th/id/OIP.Oo8kdgAQIMR3zFZYZfcWOQHaJ4?rs=1&pid=ImgDetMain",
				//			MovieTitle = "Avengers: Endgame",
				//			MovieRuntimeMinutes = 181
				//		},
				//		new Show
				//		{
				//			Id = Guid.Parse("84B0D047-C109-4D0A-9E2E-0CEAFC88AA66"),
				//			CinemaHallId = Guid.Parse("7A8D4DEA-15DD-4CDD-AD10-0AE010106891"),
				//			MovieId = Guid.Parse("8C1D9E76-419B-4CA1-844D-B9AC9207E284"),
				//			StartTime = DateTimeOffset.ParseExact("22/10/2024 17:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("22/10/2024 21:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://th.bing.com/th/id/OIP.Oo8kdgAQIMR3zFZYZfcWOQHaJ4?rs=1&pid=ImgDetMain",
				//			MovieTitle = "Avengers: Endgame",
				//			MovieRuntimeMinutes = 181
				//		},
				//		new Show
				//		{
				//			Id = Guid.Parse("84B0D047-C109-4D0A-9E2E-0CEAFC88AA67"),
				//			CinemaHallId = Guid.Parse("7A8D4DEA-15DD-4CDD-AD10-0AE010106891"),
				//			MovieId = Guid.Parse("8C1D9E76-419B-4CA1-844D-B9AC9207E284"),
				//			StartTime = DateTimeOffset.ParseExact("22/10/2024 21:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("23/10/2024 00:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://th.bing.com/th/id/OIP.Oo8kdgAQIMR3zFZYZfcWOQHaJ4?rs=1&pid=ImgDetMain",
				//			MovieTitle = "Avengers: Endgame",
				//			MovieRuntimeMinutes = 181
				//		},
				//		new Show
				//		{
				//			Id = Guid.Parse("84B0D047-C109-4D0A-9E2E-0CEAFC88AA68"),
				//			CinemaHallId = Guid.Parse("7A8D4DEA-15DD-4CDD-AD10-0AE010106891"),
				//			MovieId = Guid.Parse("0eeffa5f-f3a9-4350-8035-392ce29ba37d"),
				//			StartTime = DateTimeOffset.ParseExact("12/07/2024 17:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("12/07/2024 21:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F10-2024%2Fvenom.jpg&w=1920&q=75",
				//			MovieTitle = "VENOM: KÈO CUỐI",
				//			MovieRuntimeMinutes = 140
				//		},
				//		new Show
				//		{
				//			Id = Guid.Parse("84B0D047-C109-4D0A-9E2E-0CEAFC88AA69"),
				//			CinemaHallId = Guid.Parse("7a8d4dea-15dd-4cdd-ad10-0ae010106891"),
				//			MovieId = Guid.Parse("d98cadaf-c1d7-4a32-af1c-b732619f8cf4"),
				//			StartTime = DateTimeOffset.ParseExact("12/07/2024 21:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("13/07/2024 00:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fred-one-official-poster.jpg&w=1920&q=75",
				//			MovieTitle = "MẬT MÃ ĐỎ",
				//			MovieRuntimeMinutes = 181
				//		},
				//		new Show
				//		{
				//			Id = Guid.Parse("8d78d034-f025-4c31-8bc1-98f3c4e25720"),
				//			CinemaHallId = Guid.Parse("7a8d4dea-15dd-4cdd-ad10-0ae010106891"),
				//			MovieId = Guid.Parse("0eeffa5f-f3a9-4350-8035-392ce29ba37d"),
				//			StartTime = DateTimeOffset.ParseExact("12/07/2024 21:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("12/07/2024 00:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F10-2024%2Fvenom.jpg&w=1920&q=75",
				//			MovieTitle = "VENOM: KÈO CUỐI",
				//			MovieRuntimeMinutes = 181
				//		},
				//		new Show
				//		{
				//			Id = Guid.Parse("6cf80815-1743-469f-b52c-5164ea46549b"),
				//			CinemaHallId = Guid.Parse("7a8d4dea-15dd-4cdd-ad10-0ae010106891"),
				//			MovieId = Guid.Parse("6813144b-b5f0-4fc5-af54-c39aa4122a63"),
				//			StartTime = DateTimeOffset.ParseExact("12/07/2024 19:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("12/07/2024 21:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fthan-duoc.png&w=1920&q=75",
				//			MovieTitle = "THẦN DƯỢC",
				//			MovieRuntimeMinutes = 181
				//		},
				//		new Show
				//		{
				//			Id = Guid.Parse("6d88770b-b2a1-4a6a-85f3-0b2585c09d74"),
				//			CinemaHallId = Guid.Parse("7a8d4dea-15dd-4cdd-ad10-0ae010106891"),
				//			MovieId = Guid.Parse("a3479185-9ab4-4635-9278-2c2a3391dc35"),
				//			StartTime = DateTimeOffset.ParseExact("12/07/2024 21:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("12/07/2024 00:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fvung-dat-bi-nguyen-rua.png&w=1920&q=75",
				//			MovieTitle = "VÙNG ĐẤT BỊ NGUYỀN RỦA",
				//			MovieRuntimeMinutes = 181
				//		},
				//		new Show
				//		{
				//			Id = Guid.Parse("87bc7c72-2c60-4433-84e2-d30a2304103c"),
				//			CinemaHallId = Guid.Parse("7a8d4dea-15dd-4cdd-ad10-0ae010106891"),
				//			MovieId = Guid.Parse("647e75ce-b1f8-4c53-a1a9-98cd423bd56d"),
				//			StartTime = DateTimeOffset.ParseExact("12/07/2024 19:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			EndTime = DateTimeOffset.ParseExact("12/07/2024 21:30 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
				//			CinemaName = "7 Anh Em Cinema",
				//			HallName = "Rạp 1",
				//			MoviePosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fdoi-ban-hoc-yeu-poster.png&w=1920&q=75",
				//			MovieTitle = "ĐÔI BẠN HỌC YÊU",
				//			MovieRuntimeMinutes = 181
				//		}
				//	);
				//	context.SaveChanges();
				//}
			}
		}
	}
}
