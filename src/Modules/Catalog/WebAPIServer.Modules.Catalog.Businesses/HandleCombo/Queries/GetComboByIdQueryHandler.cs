using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Catalog.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Queries
{
    public class GetComboByIdQueryHandler : IRequestHandler<GetComboByIdQuery, OneOf<ComboForViewDetailsDto, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly IComboRepository _comboRepository;
        private readonly ILogger<GetComboByIdQueryHandler> _logger;
        public GetComboByIdQueryHandler(IMapper mapper, 
            IComboRepository comboRepository, 
            ILogger<GetComboByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _comboRepository = comboRepository;
            _logger = logger;
        }
        public async Task<OneOf<ComboForViewDetailsDto, ResponseException>> Handle(GetComboByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var combo = await _comboRepository.FindByIdAsync(request.id);
                if (combo is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.NotFound);
                }
                return _mapper.Map<ComboForViewDetailsDto>(combo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Combo>(ErrorCode.OperationFailed);
            }
        }
    }
}
