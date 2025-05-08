using Application.UseCases.Auth.DTOs;
using Domain.Entities;
using Domain.Entities.Legals;
using Domain.Exceptions;
using Domain.Interfaces.Legals;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Auth.Service;

public class LegalConsentService : ILegalConsentService
{
    private readonly ILegalDocumentRepository _legalDocumentRepository;
    private readonly IUserLegalConsentRepository _userLegalConsentRepository;
    private readonly ILogger<LegalConsentService> _logger;

    public LegalConsentService(
        ILegalDocumentRepository legalDocumentRepository,
        IUserLegalConsentRepository userLegalConsentRepository,
        ILogger<LegalConsentService> logger)
    {
        _legalDocumentRepository = legalDocumentRepository;
        _userLegalConsentRepository = userLegalConsentRepository;
        _logger = logger;
    }


    public async Task RegisterPrivacyPolicyConsentAsync(Users user, string ipAddress, int userAgentId,
        List<LegalConsentDto> legalConsents)
    {
        if (legalConsents == null || !legalConsents.Any())
        {
            _logger.LogWarning("Aucun consentement légal fourni pour l'utilisateur {UserId}", user.UserId);
            throw new BusinessException(ErrorCodes.NoLegalConsentsProvided, "legalConsents");
        }

        // Vérifier que chaque document existe et est actif
        var activeLegalDocuments = new List<LegalDocument>();
        foreach (var consent in legalConsents)
        {
            var document = await _legalDocumentRepository.GetByIdAsync(consent.DocumentId);
            if (document == null || !document.IsActive ||
                (document.ValidUntil.HasValue && document.ValidUntil < DateTime.UtcNow))
            {
                _logger.LogWarning("Document légal {DocumentId} introuvable ou inactif pour l'utilisateur {UserId}",
                    consent.DocumentId, user.UserId);
                throw new BusinessException(ErrorCodes.LegalDocumentNotFound, $"Document ID: {consent.DocumentId}");
            }

            activeLegalDocuments.Add(document);
        }

        // Enregistrer un consentement pour chaque document valide
        foreach (var document in activeLegalDocuments)
        {
            var consent = new UserLegalConsent
            {
                UserId = user.UserId,
                DocumentId = document.DocumentId,
                DocumentVersion = document.Version,
                IpAddress = ipAddress,
                UserAgentId = userAgentId,
                AcceptedAt = DateTime.UtcNow,
                Revoked = false
            };

            await _userLegalConsentRepository.AddAsync(consent);
            _logger.LogInformation(
                "Consentement enregistré pour le document {DocumentId} (Version: {Version}) pour l'utilisateur {UserId}",
                document.DocumentId, document.Version, user.UserId);
        }
    }
}