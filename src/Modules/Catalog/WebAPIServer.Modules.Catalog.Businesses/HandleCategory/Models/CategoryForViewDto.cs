using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models.Base;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models
{
	public class CategoryForViewDto : CategoryBaseDto
	{
		public Guid Id { get; set; }
	}
}