using Application.UseCases.Legals.DTOs;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Legals;

namespace Application.UseCases.Legals.UseCases;

public class GetActiveLegalDocumentUseCase
{
    private readonly ILegalDocumentRepository _legalDocumentRepository;
    private readonly ILegalDocumentClauseTranslationRepository _legalDocumentClauseTranslationRepository;
    private readonly ILegalDocumentTypeTranslationRepository _legalDocumentTypeTranslationRepository;
    private readonly ISpokenLanguageRepository _spokenLanguageRepository;
    private readonly IMapper _mapper;

    public GetActiveLegalDocumentUseCase(
        ILegalDocumentRepository legalDocumentRepository,
        ILegalDocumentClauseTranslationRepository legalDocumentClauseTranslationRepository,
        ILegalDocumentTypeTranslationRepository legalDocumentTypeTranslationRepository,
        ISpokenLanguageRepository spokenLanguageRepository,
        IMapper mapper)
    {
        _legalDocumentRepository = legalDocumentRepository;
        _legalDocumentClauseTranslationRepository = legalDocumentClauseTranslationRepository;
        _legalDocumentTypeTranslationRepository = legalDocumentTypeTranslationRepository;
        _spokenLanguageRepository = spokenLanguageRepository;
        _mapper = mapper;
    }

    public async Task<LegalDocumentDto?> ExecuteAsync(string documentTypeName, string languageCode)
    {
        var lang = await _spokenLanguageRepository.GetByCodeAsync(languageCode);
        if (lang == null) return null;

        var doc = await _legalDocumentRepository.GetActiveByTypeAsync(documentTypeName);
        if (doc == null) return null;

        var typeLabel = await _legalDocumentTypeTranslationRepository
            .GetByTypeIdAndLanguageAsync(doc.DocumentTypeId, lang.LanguageId);

        var clauseTranslations = await _legalDocumentClauseTranslationRepository
            .GetByDocumentIdAndLanguageAsync(doc.DocumentId, lang.LanguageId);

        // Mapper les clauses (triées par ordre)
        var clauseDtos = clauseTranslations
            .Where(c => c.Clause != null)
            .OrderBy(c => c.Clause!.OrderIndex)
            .Select(c => _mapper.Map<LegalClauseDto>(c))
            .ToList();


        // Mapper le document
        var dto = _mapper.Map<LegalDocumentDto>(doc);
        dto.DocumentTypeName = documentTypeName;
        dto.DocumentTypeLabel = typeLabel?.Name ?? documentTypeName;
        dto.LanguageCode = languageCode;
        dto.Clauses = clauseDtos;

        return dto;
    }

}
