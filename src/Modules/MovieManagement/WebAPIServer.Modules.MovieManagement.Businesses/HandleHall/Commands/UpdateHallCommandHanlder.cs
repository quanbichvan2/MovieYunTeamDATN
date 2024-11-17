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
    public class UpdateHallCommandHandler : IRequestHandler<UpdateHallCommand, OneOf<bool, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateHallCommandHandler> _logger;
        private readonly IValidator<HallForUpdateDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHallRepository _hallRepository;
        private readonly ISeatTypeRepository _seatTypeRepository;
        public UpdateHallCommandHandler(IMapper mapper,
            ILogger<UpdateHallCommandHandler> logger,
            IHallRepository hallRepository,
            IUnitOfWork unitOfWork,
            ISeatTypeRepository seatTypeRepository,
            IValidator<HallForUpdateDto> validator)
        {
            _mapper = mapper;
            _logger = logger;
            _hallRepository = hallRepository;
            _unitOfWork = unitOfWork;
            _seatTypeRepository = seatTypeRepository;
            _validator = validator;
        }
        public async Task<OneOf<bool, ResponseException>> Handle(UpdateHallCommand request, CancellationToken cancellationToken)
        {
            try
            {

                //    var validationResult = await _validator.ValidateAsync(request.model);
                //    if (!validationResult.IsValid)
                //    {
                //        return ResponseExceptionHelper.ErrorResponse<Hall>(ErrorCode.UpdateError, validationResult.Errors);
                //    }
                //    var hall = await _hallRepository.FindByIdAsync(request.id);
                //    if (hall is null)
                //    {
                //        return ResponseExceptionHelper.ErrorResponse<Hall>(ErrorCode.NotFound);
                //    }
                //    _mapper.Map(request.model, hall);
                //    foreach (var updatedSeat in request.model.Seats)
                //    {
                //        var seat = hall.Seats.FirstOrDefault(s => s.Id == updatedSeat.Id);
                //        if(seat != null)
                //        {
                //            if(seat.SeatTypeId != updatedSeat.SeatTypeId)
                //            {
                //                var seatType = await _seatTypeRepository.FindByIdAsync(updatedSeat.SeatTypeId);
                //                seat.SeatTypeId = updatedSeat.SeatTypeId;
                //                seat.SeatTypeName = seatType.Name;
                //            }
                //        }
                //    }
                //_hallRepository.Update(hall);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Hall>(ErrorCode.OperationFailed);
            }
        }
    }
}
