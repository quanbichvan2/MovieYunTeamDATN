using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Catalog.Domain.Entities
{
    public class Category: BaseAuditableEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
    public static class CategoryConstants
    {
        /// <summary>
        /// Nước uống
        /// </summary>
        public static Guid Drinks = Guid.Parse("1a5310dc-61b0-42fe-bbed-e5ed3475002f");
        /// <summary>
        /// Đồ ăn
        /// </summary>
        public static Guid Foods = Guid.Parse("1a5310dc-61b0-42fe-bbed-e5ed3475002d");
    }
}
