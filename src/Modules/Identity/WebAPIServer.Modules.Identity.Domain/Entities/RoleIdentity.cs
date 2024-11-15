using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Identity.Domain.Entities
{
	public class RoleIdentity : BaseEntity
	{
		public string Name { get; set; } = default!;
		public string NormalizeName { get; set; } = default!;

		public ICollection<UserRoleIdentity>? Roles { get; set; }
	}
	public class RoleIdentityConstants
	{
		public static Guid Client = Guid.Parse("588b4357-87d6-4d5f-9bb2-7db83affd6e0");
		public static Guid Administrator = Guid.Parse("95ba514b-64f9-4002-9704-11c0ec72b408");
	}
}