using MediatR;
using Microsoft.AspNetCore.Http;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Queries
{
    public record CallBackVnPayQuery(IQueryCollection collection): IRequest<VnPayCallbackDto>; 
}
