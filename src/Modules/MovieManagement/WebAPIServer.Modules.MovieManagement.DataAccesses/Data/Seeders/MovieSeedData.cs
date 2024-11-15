using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Modules.MovieManagement.Domain.Enums;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Data.Seeders
{
	internal static class MovieSeedData
	{
		public static void Initialize(this IServiceProvider serviceProvider)
		{
			using (var context = new MovieManagementDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieManagementDbContext>>()))
			{
				if (!context.Movies.Any())
				{
					var movies = new List<Movie>
				{
						new Movie
						{
							Id = Guid.Parse("8c1d9e76-419b-4ca1-844d-b9ac9207e284"),
							Title = "Avengers: Endgame",
							AgeRating = AgeRating.T13,
							RuntimeMinutes = 181,
							ReleaseDate = DateTime.ParseExact("22/04/2019 00:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
							TrailerLink = "https://www.youtube.com/watch?v=TcMBFSGVi1c",
							BannerText = "After the devastating events of Avengers: Infinity War (2018), the universe is in ruins. With the help of remaining allies, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe.",
							HeaderImage = "https://th.bing.com/th/id/OIP.SqrBOi5EUS1H70UmQKOr3AHaDt?w=328&h=175&c=7&r=0&o=5&pid=1.7",
							PosterImage = "https://th.bing.com/th/id/OIP.Oo8kdgAQIMR3zFZYZfcWOQHaJ4?rs=1&pid=ImgDetMain",
							Description = null,
							DirectorName = "Anthony Russo và Joe Russo",
							DirectorId = Guid.Parse("95b1fd33-39cc-421d-abd0-93c180ecf291"),
							Genres = new List<MovieGenre>
							{
								new MovieGenre
								{
									GenreId = GenreConstants.Action,
									GenreName = "Phim hành động"
								},
								new MovieGenre
								{
									GenreId = GenreConstants.Scifi,
									GenreName = "Phim khoa học viễn tưởng"
								}
							},
							CastMembers = new List<MovieCastMember>
							{
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("77df9e03-a19c-4957-92cb-947c19b1172a"),
									CastMemberName = "Robert John Downey Jr."
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("88749d4b-8ce9-49b0-bfcf-d33e3b010e3b"),
									CastMemberName = "Chris Evans"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("2937418b-ec60-467c-b886-745a8be0c9b3"),
									CastMemberName = "Mark Ruffalo"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("3ce74925-bcfb-44a8-9ecb-9f426124f949"),
									CastMemberName = "Chris Hemsworth"
								},
							}
						},
						new Movie
						{
							Id = Guid.Parse("d98cadaf-c1d7-4a32-af1c-b732619f8cf4"),
							Title = "MẬT MÃ ĐỎ",
							AgeRating = AgeRating.K,
							RuntimeMinutes = 123,
							ReleaseDate = DateTime.ParseExact("11/07/2024 00:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
							TrailerLink = "https://youtu.be/2T_mKyH17mY",
							BannerText = "Red One / Mật Mã Đỏ kể về Callum Drift, người đứng đầu lực lượng an ninh Bắc Cực , phải hợp tác với Jack O'Malley, một thợ săn tiền thưởng , để tìm và giải cứu Ông già Noel sau khi ông bị bắt cóc.",
							HeaderImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fred-one-official-poster.jpg&w=1920&q=75",
							PosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fred-one-official-poster.jpg&w=1920&q=75",
							Description = null,
							DirectorName = "Jake Kasdan",
							DirectorId = Guid.Parse("9970977e-80b6-46c9-929b-8eb13fd8ed25"),
							Genres = new List<MovieGenre>
							{
								new MovieGenre
								{
									GenreId = GenreConstants.Action,
									GenreName = "Phim hành động"
								},
								new MovieGenre
								{
									GenreId = GenreConstants.Comedy,
									GenreName = "Phim hài"
								}
							},
							CastMembers = new List<MovieCastMember>
							  {
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("88749d4b-8ce9-49b0-bfcf-d33e3b010e3b"),
									CastMemberName = "Chris Evans"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("13d5c3eb-1e40-477d-ae49-070b35c67f96"),
									CastMemberName = "Dwayne Johnson"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("4bb9fdc2-8d16-4f1b-9437-33063519e487"),
									CastMemberName = "Lucy Liu"
								},
							}
						},
						new Movie
						{
							Id = Guid.Parse("0eeffa5f-f3a9-4350-8035-392ce29ba37d"),
							Title = "VENOM: KÈO CUỐI",
							AgeRating = AgeRating.T13,
							RuntimeMinutes = 109,
							ReleaseDate = DateTime.ParseExact("11/07/2024 00:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
							TrailerLink = "https://youtu.be/6yCMRxGI4RA",
							BannerText = "Tom Hardy sẽ tái xuất trong bom tấn Venom: The Last Dance (Tựa Việt: Venom: Kèo Cuối) và phải đối mặt với kẻ thù lớn nhất từ trước đến nay - toàn bộ chủng tộc Symbiote",
							HeaderImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F10-2024%2Fvenom.jpg&w=1920&q=75",
							PosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F10-2024%2Fvenom.jpg&w=1920&q=75",
							Description = null,
							DirectorName = "Kelly Marcel",
							DirectorId = Guid.Parse("13701a86-a3f6-4973-a7c6-82e5570772bd"),
							Genres = new List<MovieGenre>
							{
								new MovieGenre
								{
									GenreId = GenreConstants.Action,
									GenreName = "Phim hành động"
								},
								new MovieGenre
								{
									GenreId = GenreConstants.Scifi,
									GenreName = "Phim khoa học viễn tưởng"
								}
							},
							CastMembers = new List<MovieCastMember>
							{
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("23c2be94-7a37-4f7e-baa0-d4fbd9fdde51"),
									CastMemberName = "Tom Hardy"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("241823eb-d168-4bfa-8c8c-3c0e0472ba72"),
									CastMemberName = "Juno Temple"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("5b4ea65b-66ff-4305-b766-48013ee0d255"),
									CastMemberName = "Chiwetel Ejiofor"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("46f03258-2a16-4858-82f6-5c97c6cf277a"),
									CastMemberName = "Clark Backo"
								},
							}
						},
						new Movie
						{
							Id = Guid.Parse("6813144b-b5f0-4fc5-af54-c39aa4122a63"),
							Title = "THẦN DƯỢC",
							AgeRating = AgeRating.T18,
							RuntimeMinutes = 181,
							ReleaseDate = DateTime.ParseExact("11/07/2024 00:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
							TrailerLink = "https://youtu.be/jM8-Cdes09U",
							BannerText = "Elizabeth Sparkle, minh tinh sở hữu vẻ đẹp hút hồn cùng với tài năng được mến mộ nồng nhiệt. Khi đã trải qua thời kỳ đỉnh cao, nhan sắc dần tàn phai, cô tìm đến những kẻ buôn lậu để mua một loại thuốc bí hiểm nhằm thay da đổi vận, tạo ra một phiên bản trẻ trung hơn của chính mình.",
							HeaderImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fthan-duoc.png&w=1920&q=75",
							PosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fthan-duoc.png&w=1920&q=75",
							Description = null,
							DirectorName = "Coralie Fargeat",
							DirectorId = Guid.Parse("43719e6c-78f6-444a-af6a-a2632799f8df"),
							Genres = new List<MovieGenre>
							{
								new MovieGenre
								{
									GenreId = GenreConstants.Horror,
									GenreName = "Phim hành động"
								},
								new MovieGenre
								{
									GenreId = GenreConstants.Thriller,
									GenreName = "Phim giật gân"
								}
							},
							CastMembers = new List<MovieCastMember>
							{
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("24370b73-9d2d-4276-93ff-5475b2961118"),
									CastMemberName = "Demi Moore"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("84fc04f8-05b0-4b73-ab56-14234f06aae5"),
									CastMemberName = "Margaret Qualley"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("1c8bddbd-53ad-4413-8051-d0158c07d86d"),
									CastMemberName = "Dennis Quaid"
								}
							}
						},
						new Movie
						{
							Id = Guid.Parse("a3479185-9ab4-4635-9278-2c2a3391dc35"),
							Title = "VÙNG ĐẤT BỊ NGUYỀN RỦA",
							AgeRating = AgeRating.T18,
							RuntimeMinutes = 181,
							ReleaseDate = DateTime.ParseExact("11/07/2024 00:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
							TrailerLink = "https://youtu.be/4X-hI7qCJ98",
							BannerText = "Sau cái chết của vợ, để trốn tránh quá khứ, Mit và cô con gái May chuyển đến một ngôi nhà mới ở khu phố ngoại ô. Trong lúc chuẩn bị xây dựng một miếu thờ thiên trước nhà mới, anh vô tình giải thoát một con quỷ đang giận dữ và ác mộng kinh hoàng ập tới cuộc sống mới của hai bố con. Mit tìm kiếm sự giúp đỡ của một thầy phù thủy để làm lễ trừ tà. Nhưng họ không biết rằng, lễ trừ tà cuối cùng sẽ mở ra nhiều bí mật đen tối về vùng đất này.",
							HeaderImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fvung-dat-bi-nguyen-rua.png&w=1920&q=75",
							PosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fvung-dat-bi-nguyen-rua.png&w=1920&q=75",
							Description = null,
							DirectorName = "Panu Aree",
							DirectorId = Guid.Parse("370946a7-a527-469e-91b7-15f41eeba7d7"),
							Genres = new List<MovieGenre>
							{
								new MovieGenre
								{
									GenreId = GenreConstants.Horror,
									GenreName = "Phim kinh dị"
								},
								new MovieGenre
								{
									GenreId = GenreConstants.Thriller,
									GenreName = "Phim giật gân"
								}
							},
							CastMembers = new List<MovieCastMember>
							{
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("cf0d2e94-7dd5-45e2-9151-9e0fe69876e2"),
									CastMemberName = "Ananda Everingham"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("2c4dbf91-1603-4047-b1a4-714886d08807"),
									CastMemberName = "Bront Palarae"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("48a15e1f-07a2-41b9-996a-dc14730ed532"),
									CastMemberName = "Jennis Oprasert"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("8a2cfb55-1e80-4316-91a6-aed6219f4985"),
									CastMemberName = "Firdaus Karim"
								}
								,new MovieCastMember
								{
									CastMemberId = Guid.Parse("b505ea90-1069-4ad3-979b-74106d009ca6"),
									CastMemberName = "Han Zalini"
								}
							}
						},
						new Movie
						{
							Id = Guid.Parse("647e75ce-b1f8-4c53-a1a9-98cd423bd56d"),
							Title = "ĐÔI BẠN HỌC YÊU",
							AgeRating = AgeRating.T18,
							RuntimeMinutes = 181,
							ReleaseDate = DateTime.ParseExact("11/07/2024 00:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
							TrailerLink = "https://youtu.be/4ViQtRMPl9I",
							BannerText = "Bộ phim xoay quanh đôi bạn ngỗ nghịch Jae-hee và Heung-soo cùng những khoảnh khắc “dở khóc dở cười” khi cùng chung sống trong một ngôi nhà. Jae-hee là một cô gái “cờ xanh” với tâm hồn tự do, sống hết mình với tình yêu. Ngược lại, Heung-soo lại là một “cờ đỏ” chính hiệu khi cho rằng tình yêu là sự lãng phí cảm xúc không cần thiết",
							HeaderImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fdoi-ban-hoc-yeu-poster.png&w=1920&q=75",
							PosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F11-2024%2Fdoi-ban-hoc-yeu-poster.png&w=1920&q=75",
							Description = null,
							DirectorName = "E.Oni",
							DirectorId = Guid.Parse("495222e7-80fa-419f-9a61-44849031a143"),
							Genres = new List<MovieGenre>
							{
								new MovieGenre
								{
									GenreId = GenreConstants.Comedy,
									GenreName = "Phim Hài"
								},
								new MovieGenre
								{
									GenreId = GenreConstants.Romance,
									GenreName = "Phim Lãng Mạn"
								}
							},
							CastMembers = new List<MovieCastMember>
							{
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("9e9a77a8-2478-4c0c-a1bb-a627a9fcb74f"),
									CastMemberName = "Kim Go Eun"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("fc2aa1e7-7a6b-465d-a2ec-f4d52cb5b242"),
									CastMemberName = "Steve Sanghyun Noh"
								}
							}
						},
						new Movie
						{
							Id = Guid.Parse("c3d04b90-3a27-48b2-a852-999a17f49218"),
							Title = "CÔ DÂU HÀO MÔN",
							AgeRating = AgeRating.T18,
							RuntimeMinutes = 181,
							ReleaseDate = DateTime.ParseExact("11/07/2024 00:00 +07:00", "dd/MM/yyyy HH:mm zzz", null).ToUniversalTime(),
							TrailerLink = "https://youtu.be/OP5X4Bp-g78",
							BannerText = "Uyển Ân chính thức lên xe hoa trong thế giới thượng lưu của đạo diễn Vũ Ngọc Đãng qua bộ phim Cô Dâu Hào Môn. Bộ phim xoay quanh câu chuyện làm dâu nhà hào môn dưới góc nhìn hài hước và châm biếm, hé lộ những câu chuyện kén dâu chọn rể trong giới thượng lưu.",
							HeaderImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F10-2024%2Fco-dau-hao-mon-official.png&w=1920&q=75",
							PosterImage = "https://cinestar.com.vn/_next/image/?url=https%3A%2F%2Fapi-website.cinestar.com.vn%2Fmedia%2Fwysiwyg%2FPosters%2F10-2024%2Fco-dau-hao-mon-official.png&w=1920&q=75",
							Description = null,
							DirectorName = "Vũ Ngọc Đãng",
							DirectorId = Guid.Parse("bbe9eec1-09d0-4a3b-88ff-26ddb8dab2ab"),
							Genres = new List<MovieGenre>
							{
								new MovieGenre
								{
									GenreId = GenreConstants.Horror,
									GenreName = "Phim kinh dị"
								},
								new MovieGenre
								{
									GenreId = GenreConstants.Thriller,
									GenreName = "Phim giật gân"
								}
							},
							CastMembers = new List<MovieCastMember>
							{
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("2da42624-799f-47d4-a985-b65884dbdfe5"),
									CastMemberName = "Uyển Ân"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("8f28ff1f-5a56-4dee-b09d-3300b99d8ab3"),
									CastMemberName = "Kiều Minh Tuấn"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("e41658cf-26cb-4648-8fb5-df24861a7222"),
									CastMemberName = "Thu Trang"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("aaf80a46-3b89-4cc8-a815-eb4f1e1f4001"),
									CastMemberName = "Samuel An"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("075d27e5-e31d-472f-b82e-83522394e440"),
									CastMemberName = "Lê Giang"
								},
								new MovieCastMember
								{
									CastMemberId = Guid.Parse("bca59700-bd34-40f3-81b5-dfb80576a291"),
									CastMemberName = "NSND Hồng Vân"
								}
							}
						}
					};
					context.Movies.AddRange(movies);
					context.SaveChanges();
				}
			}
		}
	}
}
