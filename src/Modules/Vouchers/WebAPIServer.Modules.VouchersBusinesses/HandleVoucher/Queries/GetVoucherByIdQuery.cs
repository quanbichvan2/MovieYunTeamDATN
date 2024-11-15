using MediatR;
using OneOf;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Queries
{
	public record GetVoucherByIdQuery(Guid Id) : IRequest<OneOf<VoucherForViewDto, ResponseException>>;
}
