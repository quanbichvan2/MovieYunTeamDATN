using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Users.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Modules.Users.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Users.Businesses.HandleUser.Queries
{
	public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserForViewDto>>
	{
		private readonly IMapper _mapper;
		private readonly IUserReponsitory _userReponsitory;
		private readonly ILogger<GetAllUsersQueryHandler> _logger;
		public GetAllUsersQueryHandler(IMapper mapper,
			IUserReponsitory userReponsitory,
			ILogger<GetAllUsersQueryHandler> logger)
		{
			_mapper = mapper;
			_userReponsitory = userReponsitory;
			_logger = logger;
		}
		public async Task<PaginatedList<UserForViewDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var query = _userReponsitory.GetAll();
				var allowedUserProperties = new List<string> { "Name" };
				if (!string.IsNullOrEmpty(request.Filter.SearchTerm))
				{
					string search = request.Filter.SearchTerm.ToLower().Trim();
					query = query.Where(x => EF.Functions.Unaccent(x.Name).ToLower().Contains(search));
				}
				query = query.SortBy(request.Filter?.SortColumn, allowedUserProperties, request.Filter.IsDescending);

				var paginatedUsers = await PaginatedList<User>.CreateAsync(
					query,
					request.Filter.PageIndex,
					request.Filter.PageSize,
					cancellationToken);

				var productViewDtos = _mapper.Map<List<UserForViewDto>>(paginatedUsers.Items);

				var paginatedUserViews = new PaginatedList<UserForViewDto>(
					productViewDtos,
					request.Filter.PageIndex,
					request.Filter.PageSize,
					paginatedUsers.TotalCount);
				return paginatedUserViews;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error fetching products: {ex.Message}");
				throw new NullReferenceException(nameof(Handle), ex);
			}
		}
	}
}
