using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Commands
{
    public record DeleteVoucherCommand(Guid Id):IRequest<OneOf<bool,ResponseException>>;
}
