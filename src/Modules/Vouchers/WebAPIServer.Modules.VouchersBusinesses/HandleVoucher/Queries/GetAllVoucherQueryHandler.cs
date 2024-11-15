using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Vouchers.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Modules.Vouchers.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Extensions;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Queries
{
	public class GetAllVoucherQueryHandler : IRequestHandler<GetAllVoucherQuery, PaginatedList<VoucherForViewDto>>
	{
		private readonly IMapper _mapper;
		private readonly IVoucherRepository _voucherRepository;
		private readonly ILogger<GetAllVoucherQueryHandler> _logger;
		public GetAllVoucherQueryHandler(
			IMapper mapper,
			IVoucherRepository voucherRepository,
			ILogger<GetAllVoucherQueryHandler> logger)
		{
			_mapper = mapper;
			_voucherRepository = voucherRepository;
			_logger = logger;
		}

		public async Task<PaginatedList<VoucherForViewDto>> Handle(GetAllVoucherQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var query = _voucherRepository.GetAll();
				var allowedVoucherProperties = new List<string> { "Name", "DiscountValue", "Code" };
				if (!string.IsNullOrEmpty(request.Filter.SearchTerm))
				{
					string search = request.Filter.SearchTerm.ToLower().Trim();
					query = query.Where(x => EF.Functions.Unaccent(x.Name).ToLower().Contains(search));
				}
				query = query.SortBy(request.Filter?.SortColumn, allowedVoucherProperties, request.Filter.IsDescending);

				var paginatedVouchers = await PaginatedList<Voucher>.CreateAsync(
					query,
					request.Filter.PageIndex,
					request.Filter.PageSize,
					cancellationToken);

				var voucherViewDtos = _mapper.Map<List<VoucherForViewDto>>(paginatedVouchers.Items);

				var paginatedVoucherViews = new PaginatedList<VoucherForViewDto>(
					voucherViewDtos,
					request.Filter.PageIndex,
					request.Filter.PageSize,
					paginatedVouchers.TotalCount);
				return paginatedVoucherViews;
			}
			catch (Exception ex)
			{

				_logger.LogError($"Error fetching vouchers: {ex.Message}");
				throw new NullReferenceException(nameof(Handle), ex);
			}
		}
	}
}
