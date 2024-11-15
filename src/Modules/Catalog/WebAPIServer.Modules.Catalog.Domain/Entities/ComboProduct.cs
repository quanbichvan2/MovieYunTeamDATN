using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Catalog.Domain.Entities
{
    public class ComboProduct : BaseAuditableEntity
    {
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice => UnitPrice * Quantity;

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public string ProductName { get; set; } = default!;
        public string ProductImage { get; set; } = default!;

        [ForeignKey("Combo")]
        public Guid ComboId { get; set; }
        public Combo? Combo { get; set; }
    }
}
