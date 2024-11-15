using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.DataAccesses.Data;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Catalog.DataAccesses.Repositories
{
	public class ComboRepository : BaseRepository<Combo, CatalogDbContext>, IComboRepository
	{
		public ComboRepository(CatalogDbContext context) : base(context) { }
		public override async Task<Combo?> FindByIdAsync(Guid? id)
		{
			return await _context.Combos
				.Include(x => x.Products)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<bool> IsProductComboExist(Guid comboId, Guid? productId)
		{
			var combo = await _context.Combos
			   .Include(x => x.Products)
			   .FirstOrDefaultAsync(p => p.Id == comboId);

			return combo?.Products.Any(x => x.ProductId == productId) ?? false;
		}

		public async Task<bool> IsUniqueComboName(string name)
		{
			var combo = await _context.Combos
				.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
			return combo == null;
		}
		public async Task<bool> IsNameExistsAsync(string name)
		{
			return await _context.Combos.AnyAsync(p => p.Name == name);
		}

		public async Task<bool> IsCodeExistsAsync(string code)
		{
			return await _context.Combos.AnyAsync(p => p.Code == code);
		}
	}
}
