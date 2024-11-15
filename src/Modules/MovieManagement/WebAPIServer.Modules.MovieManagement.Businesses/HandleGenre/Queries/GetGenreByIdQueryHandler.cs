using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Queries
{
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, OneOf<GenreForViewDto, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<GetGenreByIdQueryHandler> _logger;
        public GetGenreByIdQueryHandler(IMapper mapper, 
            IGenreRepository genreRepository, 
            ILogger<GetGenreByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
            _logger = logger;
        }
        public async Task<OneOf<GenreForViewDto, ResponseException>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var genre = await _genreRepository.FindByIdAsync(request.Id);
                if (genre is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.NotFound);
                }
                return _mapper.Map<GenreForViewDto>(genre);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.OperationFailed);
            }
        }
    }
}