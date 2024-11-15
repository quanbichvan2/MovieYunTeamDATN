using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Identity.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Identity.Businesses.Models;
using WebAPIServer.Modules.Identity.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Identity.Businesses.Services
{
	public class RoleService
	{
		private readonly IRoleRepository _roleRepository;
		private readonly IBaseUnitOfWork _unitOfWork;
		private readonly ILogger<RoleService> _logger;
		private readonly IValidator<RoleForCommand> _validator;
		public RoleService(IRoleRepository roleRepository,
			IBaseUnitOfWork unitOfWork,
			ILogger<RoleService> logger,
			IValidator<RoleForCommand> validator)
		{
			_roleRepository = roleRepository;
			_unitOfWork = unitOfWork;
			_logger = logger;
			_validator = validator;
		}
		public IEnumerable<RoleIdentity> GetAll()
		{
			return _roleRepository.GetAll();
		}
		public async Task<RoleIdentity?> GetByIdAsync(Guid? id)
		{
			return await _roleRepository.GetByIdAsync(id);
		}
		public async Task<OneOf<bool, ResponseException>> Create(RoleForCommand model)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<RoleIdentity>(ErrorCode.CreateError, validationResult.Errors);
				}
				var role = _roleRepository.GetAll().Any(x => x.Name == model.Name);
				if (role)
				{
					return ResponseExceptionHelper.ErrorResponse<RoleIdentity>(ErrorCode.Existed);
				}
				var newRole = new RoleIdentity
				{
					Name = model.Name!.ToLower(),
					NormalizeName = model.Name.ToUpperInvariant()
				};
				_roleRepository.Create(newRole);
				await _unitOfWork.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<RegisterDto>(ErrorCode.OperationFailed);
			}
		}
		public async Task<OneOf<bool, ResponseException>> Update(Guid id, RoleForCommand model)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<RoleIdentity>(ErrorCode.CreateError, validationResult.Errors);
				}
				var role = await _roleRepository.GetByIdAsync(id);
				if (role == null)
				{
					return ResponseExceptionHelper.ErrorResponse<RoleIdentity>(ErrorCode.NotFound);
				}
				role.Name = model.Name!;
				role.NormalizeName = model.Name.ToUpperInvariant();
				_roleRepository.Update(role);
				await _unitOfWork.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<RegisterDto>(ErrorCode.OperationFailed);
			}

		}
		public async Task<OneOf<bool, ResponseException>> Delete(Guid id)
		{
			try
			{
				var role = await _roleRepository.GetByIdAsync(id);
				if (role == null)
				{
					return ResponseExceptionHelper.ErrorResponse<RoleIdentity>(ErrorCode.NotFound);
				}
				var isRoleAssignedToUsers = await _roleRepository.IsRoleAssignedToAnyUserAsync(id);
				if (isRoleAssignedToUsers)
				{
					return ResponseExceptionHelper.ErrorResponse<RoleIdentity>(ErrorCode.OperationFailed, "Role is assigned to one or more users and cannot be deleted.");
				}
				_roleRepository.Delete(role);
				await _unitOfWork.SaveChangesAsync();

				// Return success
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<RoleIdentity>(ErrorCode.OperationFailed, ex.Message);
			}
		}

	}
}