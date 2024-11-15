using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Data.Seeders
{
	internal static class CastMemberSeedData
	{
		public static void Initialize(this IServiceProvider serviceProvider)
		{
			using (var context = new MovieManagementDbContext(serviceProvider
				.GetRequiredService<DbContextOptions<MovieManagementDbContext>>()))
			{
				if (!context.CastMembers.Any())
				{
					context.CastMembers.AddRange(
						new CastMember { Id = Guid.Parse("77df9e03-a19c-4957-92cb-947c19b1172a"), Name = "Robert John Downey Jr." },
						new CastMember { Id = Guid.Parse("88749d4b-8ce9-49b0-bfcf-d33e3b010e3b"), Name = "Chris Evans" },
						new CastMember { Id = Guid.Parse("2937418b-ec60-467c-b886-745a8be0c9b3"), Name = "Mark Ruffalo" },
						new CastMember { Id = Guid.Parse("3ce74925-bcfb-44a8-9ecb-9f426124f949"), Name = "Chris Hemsworth" },
						new CastMember { Id = Guid.Parse("8a158f77-5df9-4f6b-8714-5caf0a2625f2"), Name = "Scarlett Johansson" },
						new CastMember { Id = Guid.Parse("14bb67d2-e5af-4f26-837c-4e25cf63a9a8"), Name = "Jeremy Renner" },
						new CastMember { Id = Guid.Parse("7232c6af-ad17-4e59-8376-2df0800ebf5c"), Name = "Don Cheadle" },
						new CastMember { Id = Guid.Parse("ccd19b4f-2e59-438e-bcfb-41caf83fa5e3"), Name = "Paul Rudd" },
						new CastMember { Id = Guid.Parse("4d0ce4c5-bd56-4cc6-941c-50b77fdc2f73"), Name = "Brie Larson" },
						new CastMember { Id = Guid.Parse("13d5c3eb-1e40-477d-ae49-070b35c67f96"), Name = "Dwayne Johnson" },
						new CastMember { Id = Guid.Parse("4bb9fdc2-8d16-4f1b-9437-33063519e487"), Name = "Lucy Liu" },
						new CastMember { Id = Guid.Parse("23c2be94-7a37-4f7e-baa0-d4fbd9fdde51"), Name = "Tom Hardy" },
						new CastMember { Id = Guid.Parse("241823eb-d168-4bfa-8c8c-3c0e0472ba72"), Name = "Juno Temple" },
						new CastMember { Id = Guid.Parse("5b4ea65b-66ff-4305-b766-48013ee0d255"), Name = "Chiwetel Ejiofor" },
						new CastMember { Id = Guid.Parse("46f03258-2a16-4858-82f6-5c97c6cf277a"), Name = "Clark Backo" },
						new CastMember { Id = Guid.Parse("24370b73-9d2d-4276-93ff-5475b2961118"), Name = "Demi Moore" },
						new CastMember { Id = Guid.Parse("84fc04f8-05b0-4b73-ab56-14234f06aae5"), Name = "Margaret Qualley" },
						new CastMember { Id = Guid.Parse("1c8bddbd-53ad-4413-8051-d0158c07d86d"), Name = "Dennis Quaid" },
						new CastMember { Id = Guid.Parse("cf0d2e94-7dd5-45e2-9151-9e0fe69876e2"), Name = "Ananda Everingham" },
						new CastMember { Id = Guid.Parse("2c4dbf91-1603-4047-b1a4-714886d08807"), Name = "Bront Palarae" },
						new CastMember { Id = Guid.Parse("48a15e1f-07a2-41b9-996a-dc14730ed532"), Name = "Jennis Oprasert" },
						new CastMember { Id = Guid.Parse("8a2cfb55-1e80-4316-91a6-aed6219f4985"), Name = "Firdaus Karim" },
						new CastMember { Id = Guid.Parse("b505ea90-1069-4ad3-979b-74106d009ca6"), Name = "Han Zalini" },
						new CastMember { Id = Guid.Parse("9e9a77a8-2478-4c0c-a1bb-a627a9fcb74f"), Name = "Kim Go Eun" },
						new CastMember { Id = Guid.Parse("fc2aa1e7-7a6b-465d-a2ec-f4d52cb5b242"), Name = "Steve Sanghyun Noh" },
						new CastMember { Id = Guid.Parse("2da42624-799f-47d4-a985-b65884dbdfe5"), Name = "Uyển Ân" },
						new CastMember { Id = Guid.Parse("8f28ff1f-5a56-4dee-b09d-3300b99d8ab3"), Name = "Kiều Minh Tuấn" },
						new CastMember { Id = Guid.Parse("e41658cf-26cb-4648-8fb5-df24861a7222"), Name = "Thu Trang" },
						new CastMember { Id = Guid.Parse("aaf80a46-3b89-4cc8-a815-eb4f1e1f4001"), Name = "Samuel An" },
						new CastMember { Id = Guid.Parse("075d27e5-e31d-472f-b82e-83522394e440"), Name = "Lê Giang" },
						new CastMember { Id = Guid.Parse("bca59700-bd34-40f3-81b5-dfb80576a291"), Name = "NSND Hồng Vân" }
					);
					context.SaveChanges();
				}
			}
		}
	}
}
