using MediatR;
using OneOf;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Commands
{
    public record CreateVoucherCommand(VoucherForCreateDto Model) : IRequest<OneOf<Guid, ResponseException>> { }
}
