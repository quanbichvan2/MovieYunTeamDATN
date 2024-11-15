using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Queries
{
    public class GetSeatTypesAllQueryHandler : IRequestHandler<GetSeatTypesAllQuery, IEnumerable<SeatTypeForViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISeatTypeRepository _seatTypeRepository;
        private readonly ILogger<GetSeatTypesAllQueryHandler> _logger;
        public GetSeatTypesAllQueryHandler(IMapper mapper,
            ISeatTypeRepository seatTypeRepository,
            ILogger<GetSeatTypesAllQueryHandler> logger)
        {
            _seatTypeRepository = seatTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SeatTypeForViewDto>> Handle(GetSeatTypesAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var seatTypes = await _seatTypeRepository.GetAllAsync();
                var seatTypesForViewDto = _mapper.Map<IEnumerable<SeatTypeForViewDto>>(seatTypes);
                return seatTypesForViewDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching SeatTypes: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
