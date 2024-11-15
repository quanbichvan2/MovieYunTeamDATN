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
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateGenreCommandHandler> _logger;
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GenreForCreateDto> _validator;
        public CreateGenreCommandHandler(IMapper mapper,
            ILogger<CreateGenreCommandHandler> logger,
            IGenreRepository genreRepository,
            IUnitOfWork unitOfWork,
            IValidator<GenreForCreateDto> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _genreRepository = genreRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<OneOf<Guid, ResponseException>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.CreateError, validationResult.Errors);
                }
                Genre genre = _mapper.Map<Genre>(request.model);
                await _genreRepository.CreateAsync(genre);
                await _unitOfWork.SaveChangesAsync();
                return genre.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.OperationFailed);
            }
        }
    }
}