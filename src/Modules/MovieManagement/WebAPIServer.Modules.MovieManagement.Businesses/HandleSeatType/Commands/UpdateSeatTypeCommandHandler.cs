using AutoMapper;
using FluentValidation;
using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Commands
{
    public class UpdateSeatTypeCommandHandler : IRequestHandler<UpdateSeatTypeCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISeatTypeRepository _seatTypeRepository;
        private readonly IValidator<UpdateSeatTypeDto> _validator;
        private readonly IMapper _mapper;
        public UpdateSeatTypeCommandHandler(IUnitOfWork unitOfWork, ISeatTypeRepository seatTypeRepository, IMapper mapper, IValidator<UpdateSeatTypeDto> validator)
        {
            _unitOfWork = unitOfWork;
            _seatTypeRepository = seatTypeRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<OneOf<Guid, ResponseException>> Handle(UpdateSeatTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<SeatType>(ErrorCode.UpdateError, validationResult.Errors);
                }

                var seatType = await _seatTypeRepository.FindByIdAsync(request.id);
                if (seatType == null)
                {
                    return ResponseExceptionHelper.ErrorResponse<SeatType>(ErrorCode.NotFound);
                }
                _mapper.Map(request.model, seatType);
                _seatTypeRepository.Update(seatType);
                await _unitOfWork.SaveChangesAsync();

                return seatType.Id;
            }
            catch (Exception)
            {
                return ResponseExceptionHelper.ErrorResponse<SeatType>(ErrorCode.OperationFailed);
            }
        }
    }
}
