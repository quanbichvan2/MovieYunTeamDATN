using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Data.Seeders
{
    internal static class GenreSeedData
    {
        public static void Initialize(this IServiceProvider serviceProvider)
        {
            using (var context = new MovieManagementDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieManagementDbContext>>()))
            {
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(
                        new Genre
                        {
                            Id = GenreConstants.Action,
                            Name = "Phim hành động",
                            Description = "Phim có cảnh hành động nhanh và hồi hộp"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Animated,
                            Name = "Phim hoạt họa, hoạt hình",
                            Description = "Phim hoạt hình cho trẻ em và người lớn"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Comedy,
                            Name = "Phim hài",
                            Description = "Phim gây cười, hài hước"
                        },
                        new Genre
                        {
                            Id = GenreConstants.ComicBook,
                            Name = "Phim chuyển thể từ chuyện tranh",
                            Description = "Phim dựa trên các truyện tranh nổi tiếng"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Documentary,
                            Name = "Phim tài liệu",
                            Description = "Phim dựa trên các sự kiện có thật"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Drama,
                            Name = "Phim kịch tính",
                            Description = "Phim có tình huống kịch tính và đầy cảm xúc"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Horror,
                            Name = "Phim kinh dị",
                            Description = "Phim với yếu tố sợ hãi, kinh dị"
                        },
                        new Genre
                        {
                            Id = GenreConstants.KidnFamily,
                            Name = "Phim về gia đình và trẻ nhỏ",
                            Description = "Phim dành cho gia đình và trẻ nhỏ"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Musical,
                            Name = "Phim về âm nhạc",
                            Description = "Phim với yếu tố âm nhạc là chủ đạo"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Romance,
                            Name = "Phim lãng mạn",
                            Description = "Phim tình cảm, lãng mạn"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Fantasy,
                            Name = "Phim huyền ảo",
                            Description = "Phim có yếu tố thần thoại và huyền bí"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Scifi,
                            Name = "Phim khoa học viễn tưởng",
                            Description = "Phim với yếu tố khoa học viễn tưởng"
                        },
                        new Genre
                        {
                            Id = GenreConstants.Thriller,
                            Name = "Phim giật gân",
                            Description = "Phim hồi hộp và kịch tính"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}