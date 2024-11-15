using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Catalog.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Image { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; } = default;

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; } = default!;
        public Category Category { get; set; } = default!;
    }
}