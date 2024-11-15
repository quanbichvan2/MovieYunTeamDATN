using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.MovieManagement.Domain.Entities
{
    public class SeatType: BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
        public double Price { get; set; }
    }
    public static class SeatTypeConstants
    {
        /// <summary>
        /// Ghế thường
        /// </summary>
        public static Guid Regular = Guid.Parse("aaa00a00-00a0-0a00-0000-0a00ccdc000a");
        /// <summary>
        /// Ghế VIP
        /// </summary>
        public static Guid Vip = Guid.Parse("aaa00a00-00a0-0a00-0000-0a00ccdc000b");
        /// <summary>
        /// Ghế cặp
        /// </summary>
        public static Guid Couple = Guid.Parse("aaa00a00-00a0-0a00-0000-0a00ccdc000c");
    }
}