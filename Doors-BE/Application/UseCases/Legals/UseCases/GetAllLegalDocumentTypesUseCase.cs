using Application.UseCases.Legals.DTOs;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Legals;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Legals.UseCases;

public class GetAllLegalDocumentTypesUseCase
{
    private readonly ILegalDocumentTypeRepository _legalDocumentTypeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllLegalDocumentTypesUseCase> _logger;
   

    public GetAllLegalDocumentTypesUseCase(
        ILegalDocumentTypeRepository legalDocumentTypeRepository,
        IMapper mapper,
        ILogger<GetAllLegalDocumentTypesUseCase> logger)
    {
        _legalDocumentTypeRepository = legalDocumentTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LegalDocumentTypeDto>> ExecuteAsync(string languageCode)
    {
        var types = await _legalDocumentTypeRepository.GetAllWithTranslationAsync(languageCode);
        _logger.LogDebug("→ Types récupérés : {Count}", types.Count);
        return _mapper.Map<List<LegalDocumentTypeDto>>(types);

    }
}