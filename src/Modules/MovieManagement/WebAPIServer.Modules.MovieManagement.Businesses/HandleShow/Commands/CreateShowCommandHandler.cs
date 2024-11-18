using OneOf;
using MediatR;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Shared.Abstractions.Exceptions;
using FluentValidation;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using MediatR.Wrappers;
using AutoMapper;
using System;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Commands
{
    public class CreateShowCommandHandler : IRequestHandler<CreateShowCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieRepository _movieRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IShowRepository _showRepository;
        private readonly ILogger<CreateShowCommandHandler> _logger;
        private readonly IValidator<ShowForCreateDto> _validator;
        private readonly IMapper _mapper;

        public CreateShowCommandHandler(IUnitOfWork unitOfWork,
            IMovieRepository movieRepository,
            ILogger<CreateShowCommandHandler> logger,
            IValidator<ShowForCreateDto> validator,
            IMapper mapper,
            IHallRepository hallRepository,
            IShowRepository showRepository)
        {
            _unitOfWork = unitOfWork;
            _movieRepository = movieRepository;
            _logger = logger;
            _validator = validator;
            _mapper = mapper;
            _hallRepository = hallRepository;
            _showRepository = showRepository;
        }
        public async Task<OneOf<Guid, ResponseException>> Handle(CreateShowCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.Model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.CreateError, validationResult.Errors);
                }
                var showDto = request.Model;
                var movie = await _movieRepository.FindByIdAsync(showDto.MovieId);
                if (movie == null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.NotFound);
                }
                var timeDto = DateTime.SpecifyKind(showDto.StartTime.DateTime, DateTimeKind.Utc);

                var timeDuration = DateTime.SpecifyKind(showDto.StartTime.DateTime, DateTimeKind.Utc).AddMinutes(movie.RuntimeMinutes + 15);
                var checkTime = _showRepository.GetAll().Any(x => x.CinemaHallId == showDto.CinemaHallId
                    && (timeDto <= x.StartTime && timeDuration >= x.StartTime)
                    || (timeDto >= x.StartTime && timeDto <= x.StartTime.AddMinutes(movie.RuntimeMinutes + 15)));
                if (checkTime)
                {
                    return ResponseExceptionHelper.ErrorResponse<Show>(ErrorCode.Duplicated);
                }

                var show = _mapper.Map<Show>(showDto);
                show.StartTime = DateTime.SpecifyKind(showDto.StartTime.DateTime, DateTimeKind.Utc);
                await _showRepository.CreateAsync(show);
                await _unitOfWork.SaveChangesAsync();
                return show.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Show>(ErrorCode.OperationFailed);
            }
        }
    }
}
