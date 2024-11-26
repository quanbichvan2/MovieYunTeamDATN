using MediatR;
using OneOf;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Commands
{
    public record CreateSeatTypeCommand(CreateSeatTypeDto model) : IRequest<OneOf<Guid, ResponseException>>;

    public class CreateSeatTypeDto
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
