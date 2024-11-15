using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models.Base;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models
{
    public class ComboForViewDto : ComboBaseDto
    {
        public Guid Id { get; set; }
        
    }
    public class ComboForViewDetailsDto : ComboForViewDto
    {
        public IList<ComboProductForViewDto> Products { get; set; } = new List<ComboProductForViewDto>();
    }
    public class ComboProductForViewDto
    {
        public Guid Id { get; set; }
        public string Image { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
