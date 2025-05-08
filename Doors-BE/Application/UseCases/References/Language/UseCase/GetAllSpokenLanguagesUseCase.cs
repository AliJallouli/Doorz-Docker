
using Application.UseCases.References.Language.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.References.Language.UseCase;

public class GetAllSpokenLanguagesUseCase
{
    private readonly ISpokenLanguageRepository _languageRepository;
    private readonly IMapper _mapper;

    public GetAllSpokenLanguagesUseCase(
        ISpokenLanguageRepository languageRepository,
        IMapper mapper)
    {
        _languageRepository = languageRepository;
        _mapper = mapper;
    }

    public async Task<List<SpokenLanguageDto>> ExecuteAsync()
    {
        var spokenLanguages = await _languageRepository.GetAllAsync();

        return _mapper.Map<List<SpokenLanguageDto>>(spokenLanguages);
    }
}