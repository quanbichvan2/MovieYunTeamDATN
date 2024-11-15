using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.MovieManagement.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Queries
{
	public class GetCastMemberByIdQueryHandler : IRequestHandler<GetDirectorByIdQuery, OneOf<DirectorForViewDto, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly IDirectorRepository _directorRepository;
		private readonly ILogger<GetCastMemberByIdQueryHandler> _logger;
		public GetCastMemberByIdQueryHandler(IMapper mapper,
			IDirectorRepository directorRepository,
			ILogger<GetCastMemberByIdQueryHandler> logger)
		{
			_mapper = mapper;
			_directorRepository = directorRepository;
			_logger = logger;
		}
		public async Task<OneOf<DirectorForViewDto, ResponseException>> Handle(GetDirectorByIdQuery request, CancellationToken cancellationToken)
		{

			try
			{
				var director = await _directorRepository.FindByIdAsync(request.Id);
				if (director is null)
				{
					return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.NotFound);
				}
				return _mapper.Map<DirectorForViewDto>(director);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<Director>(ErrorCode.OperationFailed);
			}
		}
	}
}