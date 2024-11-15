using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Commands
{
    public class CreateHallCommandHandler : IRequestHandler<CreateHallCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateHallCommandHandler> _logger;
        private readonly IValidator<HallForCreateDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHallRepository _hallRepository;
        private readonly ISeatTypeRepository _seatTypeRepository;
        public CreateHallCommandHandler(IMapper mapper,
            ILogger<CreateHallCommandHandler> logger,
            IValidator<HallForCreateDto> validator,
            IUnitOfWork unitOfWork,
            IHallRepository hallRepository,
            ISeatTypeRepository seatTypeRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _hallRepository = hallRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _seatTypeRepository = seatTypeRepository;
        }
        public async Task<OneOf<Guid, ResponseException>> Handle(CreateHallCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<Hall>(ErrorCode.CreateError, validationResult.Errors);
                }
                Hall hall = _mapper.Map<Hall>(request.model);
                var seatType = await _seatTypeRepository.FindByIdAsync(SeatTypeConstants.Regular);
                hall.InitializeSeats(request.model.SeatColumn, request.model.SeatRow, seatType.Name, seatType.Price);
                await _hallRepository.CreateAsync(hall);
                await _unitOfWork.SaveChangesAsync();
                return hall.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.OperationFailed);
            }
        }
    }
}
