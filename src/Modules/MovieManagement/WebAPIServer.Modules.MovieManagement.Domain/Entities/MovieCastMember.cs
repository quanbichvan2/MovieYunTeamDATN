using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.MovieManagement.Domain.Entities
{
    public class MovieCastMember: BaseAuditableEntity
    {
        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = default!;

        [ForeignKey("CastMember")]
        public Guid CastMemberId { get; set; }
        public CastMember CastMember { get; set; } = default!;
        public string CastMemberName { get; set; } = default!;
    }
}
