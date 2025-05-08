using Application.UseCases.Invitation.SuperAdmin.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.References.EntityType.UseCase;

public class GetRolesByEntityTypeNameUseCase
{
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    public GetRolesByEntityTypeNameUseCase(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleDto>> ExecuteAsync(string entityTypeName, string preferredLanguageCode)
    {
        var entityTypes = await _roleRepository.GetRolesByEntityTypeNameAsync(entityTypeName, preferredLanguageCode);
        return _mapper.Map<IEnumerable<RoleDto>>(entityTypes);
    }
}