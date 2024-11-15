using System.ComponentModel.DataAnnotations;

namespace WebAPIServer.Shared.Abstractions.Entities
{
    public abstract class BaseEntity: IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}