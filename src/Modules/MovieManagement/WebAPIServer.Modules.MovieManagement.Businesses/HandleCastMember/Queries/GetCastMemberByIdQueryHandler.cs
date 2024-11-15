using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Queries
{
    public class GetCastMemberByIdQueryHandler : IRequestHandler<GetCastMemberByIdQuery, OneOf<CastMemberForViewDto, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly ICastMemberRepository _castMemberRepository;
        private readonly ILogger<GetCastMemberByIdQueryHandler> _logger;
        public GetCastMemberByIdQueryHandler(IMapper mapper, 
            ICastMemberRepository castMemberRepository, 
            ILogger<GetCastMemberByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _castMemberRepository = castMemberRepository;
            _logger = logger;
        }
        public async Task<OneOf<CastMemberForViewDto, ResponseException>> Handle(GetCastMemberByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var castMember = await _castMemberRepository.FindByIdAsync(request.Id);
                if (castMember is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.NotFound);
                }
                return _mapper.Map<CastMemberForViewDto>(castMember);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<CastMember>(ErrorCode.OperationFailed);
            }
        }
    }
}