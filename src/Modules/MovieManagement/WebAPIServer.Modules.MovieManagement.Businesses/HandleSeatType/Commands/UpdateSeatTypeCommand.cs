using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Commands
{
    public record UpdateSeatTypeCommand(Guid id, UpdateSeatTypeDto model) : IRequest<OneOf<Guid, ResponseException>>;

    public class UpdateSeatTypeDto
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
