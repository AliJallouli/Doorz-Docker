using Application.UseCases.References.EntityType.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.References.EntityType.UseCase;

public class GetAllEntityTypesUseCase
{
    private readonly IEntityTypeRepository _entityTypeRepository;
    private readonly IMapper _mapper;

    public GetAllEntityTypesUseCase(IEntityTypeRepository entityTypeRepository, IMapper mapper)
    {
        _entityTypeRepository = entityTypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EntityTypeDto>> ExecuteAsync(string language)
    {
        var entityTypes = await _entityTypeRepository.GetAllAsync(language);
        return _mapper.Map<IEnumerable<EntityTypeDto>>(entityTypes);
    }
}