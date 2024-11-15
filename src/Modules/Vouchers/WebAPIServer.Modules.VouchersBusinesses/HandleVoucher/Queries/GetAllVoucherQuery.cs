using MediatR;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Queries
{
    public record GetAllVoucherQuery(Filter Filter) : IRequest<PaginatedList<VoucherForViewDto>>;
}
