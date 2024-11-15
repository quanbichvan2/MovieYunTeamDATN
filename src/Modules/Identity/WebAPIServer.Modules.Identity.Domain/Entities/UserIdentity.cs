using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Identity.Domain.Entities
{
	public class UserIdentity : BaseEntity
	{
		public string Email { get; set; } = default!;
		public string PasswordHash { get; set; } = default!;
		public string? RefreshToken { get; set; } = default!;
		public DateTime? RefreshTokenExpiry { get; set; }

		public ICollection<UserRoleIdentity>? Roles { get; set; }
	}
}