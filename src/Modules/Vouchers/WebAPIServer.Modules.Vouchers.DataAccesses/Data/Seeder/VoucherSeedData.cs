using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Vouchers.Domain.Entities;

namespace WebAPIServer.Modules.Vouchers.DataAccesses.Data.Seeder
{
	public class VoucherSeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new VoucherDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<VoucherDbContext>>()))
			{
				if (!context.Vouchers.Any())
				{
					context.Vouchers.AddRange(
					new Voucher
					{
						Id = VouchersConstants.Student,
						Name = "Voucher cho học sinh sinh viên",
						Code = "HSMOVIE30",
						DiscountValue = 30,
						IsDiscountPercentage = true,
						CreatedAt = DateTime.UtcNow,
						ModifiedAt = DateTime.UtcNow
					},
					new Voucher
					{
						Id = VouchersConstants.Couple,
						Name = "Voucher Cho các Cặp đôi",
						Code = "CPMOVIE10",
						DiscountValue = 10,
						IsDiscountPercentage = true,
						CreatedAt = DateTime.UtcNow,
						ModifiedAt = DateTime.UtcNow
					},
					new Voucher
					{
						Id = VouchersConstants.Holiday,
						Name = "Vé Ngày lễ",
						Code = "HLMOVIE10",
						DiscountValue = 10,
						IsDiscountPercentage = true,
						CreatedAt = DateTime.UtcNow,
						ModifiedAt = DateTime.UtcNow
					}
				);
					context.SaveChanges();
				}
			}
		}
	}
}
