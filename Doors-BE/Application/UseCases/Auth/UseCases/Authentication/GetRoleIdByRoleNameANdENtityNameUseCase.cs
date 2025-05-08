using Application.UseCases.Auth.DTOs;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.UseCases.Auth.UseCases.Authentication;

public class GetRoleIdByRoleNameANdENtityNameUseCase
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleIdByRoleNameANdENtityNameUseCase(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleIdResponseDto> ExecuteAsync(RoleIdRequestDto request)
    {
        var roleId =
            await _roleRepository.GetRoleIdByRoleNameAndEntityTypeName(request.RoleName, request.EntityTypeName);

        if (!roleId.HasValue) throw new BusinessException(ErrorCodes.RoleNotFound, "RoleId");

        return new RoleIdResponseDto
        {
            RoleId = roleId.Value
        };
    }
}