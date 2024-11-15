using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models.Base;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models
{
    public class ComboForCreateDto : ComboBaseDto
    {
        public IList<ComboProductForCreateDto> Products { get; set; } = new List<ComboProductForCreateDto>();
    }
    public class ComboProductForCreateDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
