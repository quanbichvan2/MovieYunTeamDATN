using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Queries
{
    public class GetSeatTypeByIdQueryHandler : IRequestHandler<GetSeatTypeByIdQuery, SeatTypeForViewDto>
    {
        private readonly IMapper _mapper;
        private readonly ISeatTypeRepository _seatTypeRepository;
        private readonly ILogger<GetSeatTypeByIdQueryHandler> _logger;
        public GetSeatTypeByIdQueryHandler(IMapper mapper,
            ISeatTypeRepository seatTypeRepository,
            ILogger<GetSeatTypeByIdQueryHandler> logger)
        {
            _seatTypeRepository = seatTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SeatTypeForViewDto> Handle(GetSeatTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error get SeatType: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
