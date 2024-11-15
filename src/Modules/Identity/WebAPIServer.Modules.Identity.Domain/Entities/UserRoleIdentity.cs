using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Identity.Domain.Entities
{
	public class UserRoleIdentity : BaseEntity
	{
		[ForeignKey("UserIdentity")]
		public Guid UserId { get; set; }
		public UserIdentity UserIdentity { get; set; } = default!;

		[ForeignKey("RoleIdentity")]
		public Guid RoleId { get; set; }
		public RoleIdentity RoleIdentity { get; set; } = default!;
	}
}