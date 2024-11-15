using Microsoft.EntityFrameworkCore;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.DataAccesses.Data;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Catalog.DataAccesses.Repositories
{
	public class ProductRepository : BaseRepository<Product, CatalogDbContext>, IProductRepository
	{
		public ProductRepository(CatalogDbContext context) : base(context) { }

		public async Task<bool> IsNameExistsAsync(string name)
		{
			return await _context.Products
			 .AnyAsync(p => p.Name == name);
		}
		public async Task<bool> IsCodeExistsAsync(string code)
		{
			return await _context.Products
			.AnyAsync(p => p.Code == code);
		}

		public async Task<bool> IsNameExistsAsyncForUpdate(string name, Guid id)
		{
			return await _context.Products
			 .AnyAsync(p => p.Name == name && p.Id != id);
		}

		public async Task<bool> IsCodeExistsAsyncForUpdate(string code, Guid id)
		{
			return await _context.Products
			  .AnyAsync(p => p.Code == code && p.Id == id);
		}
	}
}