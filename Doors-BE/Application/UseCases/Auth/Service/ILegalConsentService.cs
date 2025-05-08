using Application.UseCases.Auth.DTOs;
using Domain.Entities;

namespace Application.UseCases.Auth.Service;

public interface ILegalConsentService
{
    Task RegisterPrivacyPolicyConsentAsync(Users user, string ipAddress, int userAgentId, List<LegalConsentDto> legalConsents);}