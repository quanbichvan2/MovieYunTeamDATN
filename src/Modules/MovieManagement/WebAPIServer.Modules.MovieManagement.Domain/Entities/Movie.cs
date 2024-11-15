using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Modules.MovieManagement.Domain.Enums;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.MovieManagement.Domain.Entities
{
	public class Movie : BaseAuditableEntity
	{
		public string Title { get; set; } = default!;
		public AgeRating AgeRating { get; set; }
		public DateTime ReleaseDate { get; set; }
		public byte RuntimeMinutes { get; set; }
		public string TrailerLink { get; set; } = default!;
		public string HeaderImage { get; set; } = default!;
		public string PosterImage { get; set; } = default!;
		public string? Description { get; set; }
		public string? BannerText { get; set; }

		[ForeignKey("Director")]
		public Guid DirectorId { get; set; }
		public Director Director { get; set; } = default!;
		public string DirectorName { get; set; } = default!;

		public ICollection<Show>? Shows { get; set; } = new List<Show>();
		public ICollection<MovieCastMember> CastMembers { get; set; } = new List<MovieCastMember>();
		public ICollection<MovieGenre> Genres { get; set; } = new List<MovieGenre>();
	}
}
