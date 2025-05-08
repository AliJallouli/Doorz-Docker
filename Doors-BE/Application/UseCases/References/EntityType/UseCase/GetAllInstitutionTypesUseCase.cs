
using Application.UseCases.References.EntityType.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.References.EntityType.UseCase;

public class GetAllInstitutionTypesUseCase
{
    private readonly IInstitutionTypeRepository _institutionTypeRepository;
    private readonly IMapper _mapper;

    public GetAllInstitutionTypesUseCase(IInstitutionTypeRepository institutionTypeRepository, IMapper mapper)
    {
        _institutionTypeRepository = institutionTypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InstitutionTypeDto>> ExecuteAsync(string language)
    {
        var entityTypes = await _institutionTypeRepository.GetAllAsync(language);
        return _mapper.Map<IEnumerable<InstitutionTypeDto>>(entityTypes);
    }
}