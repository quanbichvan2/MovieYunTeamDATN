using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Catalog.Domain.Entities
{
    public class Combo : BaseAuditableEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }

        public ICollection<ComboProduct> Products { get; set; } = new List<ComboProduct>();
    }
}
