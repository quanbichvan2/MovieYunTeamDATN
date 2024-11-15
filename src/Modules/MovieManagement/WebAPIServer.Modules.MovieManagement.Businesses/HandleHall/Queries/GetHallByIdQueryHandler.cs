using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Queries;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Shared.Abstractions.Exceptions;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Queries
{
    public class GetHallByIdQueryHandler : IRequestHandler<GetHallByIdQuery, OneOf<HallForViewDetailsDto, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly IHallRepository _hallRepository;
        private readonly ILogger<GetHallByIdQueryHandler> _logger;
        public GetHallByIdQueryHandler(IMapper mapper,
            IHallRepository hallRepository,
            ILogger<GetHallByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _hallRepository = hallRepository;
            _logger = logger;
        }
        public async Task<OneOf<HallForViewDetailsDto, ResponseException>> Handle(GetHallByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var hall = await _hallRepository.FindByIdAsync(request.Id);
                if (hall is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.NotFound);
                }
                var hallForView = _mapper.Map<HallForViewDetailsDto>(hall);
                return hallForView;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Genre>(ErrorCode.OperationFailed);
            }
        }
    }
}
