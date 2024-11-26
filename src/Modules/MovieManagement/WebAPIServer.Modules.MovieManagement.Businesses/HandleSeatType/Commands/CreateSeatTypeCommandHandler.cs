using AutoMapper;
using FluentValidation;
using MediatR;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Commands
{
    public class CreateSeatTypeCommandHandler : IRequestHandler<CreateSeatTypeCommand, OneOf<Guid, ResponseException>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISeatTypeRepository _seatTypeRepository;
        private readonly IValidator<CreateSeatTypeDto> _validator;
        private readonly IMapper _mapper;
        public CreateSeatTypeCommandHandler(IUnitOfWork unitOfWork, ISeatTypeRepository seatTypeRepository, IMapper mapper, IValidator<CreateSeatTypeDto> validator)
        {
            _unitOfWork = unitOfWork;
            _seatTypeRepository = seatTypeRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<OneOf<Guid, ResponseException>> Handle(CreateSeatTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request.model);
                if (!validationResult.IsValid)
                {
                    return ResponseExceptionHelper.ErrorResponse<SeatType>(ErrorCode.CreateError, validationResult.Errors);
                }
                var seatType = _mapper.Map<SeatType>(request.model);
                await _seatTypeRepository.CreateAsync(seatType);
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
