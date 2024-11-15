using AutoMapper;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie
{
    public class MovieProfile: Profile
    {
        public MovieProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<Movie, MovieForViewDto>()
                .ForMember(src => src.Shows, opt => opt.MapFrom(src => src.Shows))
				.ForMember(src => src.Genres, opt => opt.MapFrom(src => src.Genres));
            CreateMap<Movie, MovieForViewDetailDto>()
                .ForMember(src => src.CastMembers, opt => opt.MapFrom(src => src.CastMembers));;
            CreateMap<MovieCastMember, MovieCastMemberForViewDto>()
                .ForMember(src => src.Id, opt => opt.MapFrom(src => src.CastMemberId))
                .ForMember(src => src.Name, opt => opt.MapFrom(src => src.CastMemberName));
            CreateMap<MovieGenre, MovieGenreForViewDto>()
                .ForMember(src => src.Id, opt => opt.MapFrom(src => src.GenreId))
                .ForMember(src => src.Name, opt => opt.MapFrom(src => src.GenreName));
			CreateMap<Show, MovieShowForViewDto>()
				.ForMember(src => src.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(src => src.StartTime, opt => opt.MapFrom(src => src.StartTime));

			CreateMap<MovieForCreateDto, Movie>()
                .ForMember(src => src.Genres, opt => opt.MapFrom(src => src.Genres))
                .ForMember(src => src.CastMembers, opt => opt.MapFrom(src => src.CastMembers));
            CreateMap<MovieCastMemberForCreateDto, MovieCastMember>()
                .ForMember(src => src.CastMemberName, opt => opt.MapFrom(src => src.Name))
                .ForMember(src => src.CastMemberId, opt => opt.MapFrom(src => src.Id))
                .ForMember(src => src.MovieId, opt => opt.Ignore())
                .ForMember(src => src.Movie, opt => opt.Ignore());
                //.ForMember(src => src.Id, opt => opt.Ignore())
            CreateMap<MovieGenreForCreateDto, MovieGenre>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ForMember(src => src.GenreId, opt => opt.MapFrom(src => src.Id))
                .ForMember(src => src.MovieId, opt => opt.Ignore())
                .ForMember(src => src.Movie, opt => opt.Ignore());

            CreateMap<MovieForUpdateDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.CastMembers, opt => opt.Ignore());

            CreateMap<MovieCastMemberForUpdateDto, MovieCastMember>()
                .ForMember(dest => dest.CastMemberId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MovieId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Movie, opt => opt.Ignore());

            CreateMap<MovieGenreForUpdateDto, MovieGenre>()
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MovieId, opt => opt.Ignore())
                .ForMember(dest => dest.Movie, opt => opt.Ignore());
        }
    }
}
