using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Commands
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, OneOf<bool, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateGenreCommandHandler> _logger;
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GenreForUpdateDto> _validator;
        public UpdateGenreCommandHandler(IMapper mapper, 
            ILogger<UpdateGenreCommandHandler> logger, 
            IGenreRepository genreRepository, 
            IUnitOfWork unitOfWork, 
            IValidator<GenreForUpdateDto> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
			try
			{
                var validationResult = await _validator.ValidateAsync(request.model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.UpdateError, validationResult.Errors);
                }
                var genre = await _genreRepository.FindByIdAsync(request.id);
                if (genre is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.NotFound);
                }
                _mapper.Map(request.model, genre);
                _genreRepository.Update(genre);
                await _unitOfWork.SaveChangesAsync();
                return true;
			}
			catch (Exception ex)
			{
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.OperationFailed);
            }
        }
    }
}