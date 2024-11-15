using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models.Base;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models
{
    public sealed class CastMemberForViewDto : CastMemberBaseDto
    {
        public Guid Id { get; set; }
    }
}