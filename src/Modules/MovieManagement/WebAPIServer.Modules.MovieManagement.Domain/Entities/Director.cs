using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.MovieManagement.Domain.Entities
{
    public class Director: BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
    }
}   