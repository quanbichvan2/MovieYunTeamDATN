using System.ComponentModel.DataAnnotations;

namespace WebAPIServer.Shared.Abstractions.Entities
{
    public abstract class BaseAuditableEntity: BaseEntity
    {
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        /// <summary>
        /// Default: true
        /// </summary>
        public bool IsActived { get; set; } = true;
        /// <summary>
        /// Default: false
        /// </summary>
        public bool IsPublised { get; set; } = false;
    }
}