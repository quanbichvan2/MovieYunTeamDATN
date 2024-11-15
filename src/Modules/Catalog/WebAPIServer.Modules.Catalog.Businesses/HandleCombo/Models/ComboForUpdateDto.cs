using System.Text.Json.Serialization;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models.Base;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models
{
    public class ComboForUpdateDto : ComboBaseDto
    {
        public IList<ComboProductForUpdateDto> Products { get; set; } = new List<ComboProductForUpdateDto>();
    }
    public class ComboProductForUpdateDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
