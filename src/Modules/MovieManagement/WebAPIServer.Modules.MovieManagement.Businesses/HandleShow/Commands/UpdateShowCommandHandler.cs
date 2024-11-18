using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using System.Linq;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Commands
{
	public class UpdateShowCommandHandler : IRequestHandler<UpdateShowCommand, OneOf<bool, ResponseException>>
	{
		private readonly IShowRepository _showRepository;
		private readonly IMovieRepository _movieRepository;
		private readonly IHallRepository _hallRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IValidator<ShowForUpdateDto> _validator;
		private readonly ILogger<UpdateShowCommandHandler> _logger;

		public UpdateShowCommandHandler(IShowRepository showRepository,
			ILogger<UpdateShowCommandHandler> logger,
			IMovieRepository movieRepository,
			IHallRepository hallRepository,
			IValidator<ShowForUpdateDto> validator,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_showRepository = showRepository;
			_logger = logger;
			_movieRepository = movieRepository;
			_hallRepository = hallRepository;
			_validator = validator;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<OneOf<bool, ResponseException>> Handle(UpdateShowCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.CreateError, validationResult.Errors);
				}
				var showDto = request.Model;
				var show = await _showRepository.GetByIdAsync(request.Id);
				if (show == null)
				{
					return ResponseExceptionHelper.ErrorResponse<Show>(ErrorCode.NotFound);
				}

				var movie = await _movieRepository.FindByIdAsync(showDto.MovieId);
				if (movie == null)
				{
					return ResponseExceptionHelper.ErrorResponse<Movie>(ErrorCode.NotFound);
				}

                //var hall = await _hallRepository.FindByIdAsync(showDto);
                //if (hall == null)
                //{
                //	return ResponseExceptionHelper.ErrorResponse<Hall>(ErrorCode.NotFound);
                //}

                var timeDto = DateTime.SpecifyKind(showDto.StartTime.DateTime, DateTimeKind.Utc);

				var timeDuration = DateTime.SpecifyKind(showDto.StartTime.DateTime, DateTimeKind.Utc).AddMinutes(movie.RuntimeMinutes + 15);
				var checkTime = _showRepository.GetAll().Where(x => x.Id != show.Id).Any(x => x.CinemaHallId == showDto.CinemaHallId
					&& (timeDto <= x.StartTime && timeDuration >= x.StartTime)
					|| (timeDto >= x.StartTime && timeDto <= x.StartTime.AddMinutes(movie.RuntimeMinutes + 15)));
				if (checkTime)
				{
					return ResponseExceptionHelper.ErrorResponse<Show>(ErrorCode.Duplicated);
				}

				show.StartTime = DateTime.SpecifyKind(showDto.StartTime.DateTime, DateTimeKind.Utc);
                show.CinemaHallId = showDto.CinemaHallId;
				show.MovieId = showDto.MovieId;
                _showRepository.Update(show);
                await _unitOfWork.SaveChangesAsync();
                return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<Show>(ErrorCode.OperationFailed);
			}
		}
	}
}
