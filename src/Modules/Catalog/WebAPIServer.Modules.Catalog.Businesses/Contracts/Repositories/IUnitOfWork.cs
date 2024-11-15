using Microsoft.EntityFrameworkCore;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories
{
    public interface IUnitOfWork : IBaseUnitOfWork
	{
    }
}