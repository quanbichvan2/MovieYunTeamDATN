using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Queries
{
    public class GetHallsAllQueryHandler : IRequestHandler<GetHallsAllQuery, IEnumerable<HallForViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly IHallRepository _hallRepository;
        private readonly ILogger<GetHallsAllQueryHandler> _logger;
        public GetHallsAllQueryHandler(IMapper mapper,
            IHallRepository hallRepository,
            ILogger<GetHallsAllQueryHandler> logger)
        {
            _mapper = mapper;
            _hallRepository = hallRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<HallForViewDto>> Handle(GetHallsAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var halls = await _hallRepository.GetAllAsync();

                var hallForViewDto = _mapper.Map<IEnumerable<HallForViewDto>>(halls);
                return hallForViewDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching halls: {ex.Message}");
                throw new NullReferenceException(nameof(Handle), ex);
            }
        }
    }
}
