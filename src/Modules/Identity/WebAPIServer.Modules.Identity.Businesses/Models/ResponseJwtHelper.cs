namespace WebAPIServer.Modules.Identity.Businesses.Models
{
	public class ResponseJwtHelper
	{
		public string? Message { get; set; }
		public bool IsSuccess { get; set; }
		public string? Token { get; set; }
	}
}
