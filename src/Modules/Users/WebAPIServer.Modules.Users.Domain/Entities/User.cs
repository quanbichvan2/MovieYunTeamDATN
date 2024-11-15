using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Users.Domain.Entities
{
	public class User : BaseAuditableEntity
	{
		public string Email { get; set; } = default!;
		public string Name { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
	}
}
