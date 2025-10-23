using Application.UseCases.Invitation.Request.DTOs;
using AutoMapper;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace Application.UseCases.Invitation.Request.UseCases;

public class GetInvitationRequestsByStatusUseCase
{
    private readonly IInvitationRequestRepository _invitationRequestRepository;
    private readonly ILogger<GetInvitationRequestsByStatusUseCase> _logger;
    private readonly IMapper _mapper;

    public GetInvitationRequestsByStatusUseCase(
        IInvitationRequestRepository repository,
        ILogger<GetInvitationRequestsByStatusUseCase> logger,
        IMapper mapper)
    {
        _invitationRequestRepository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PagedResult<InvitationRequestDto>> ExecuteAsync(
        InvitationRequestStatus? status = null,
        string? entityTypeName = null,
        int page = 1,
        int pageSize = 10)
    {
        _logger.LogInformation(
            "Recherche des demandes avec le statut : {Status}, type d'entité : {EntityTypeName}, page : {Page}, pageSize : {PageSize}",
            status?.ToString() ?? "tous",
            entityTypeName ?? "tous",
            page,
            pageSize);

        // Validation des paramètres de pagination
        if (page < 1)
            throw new ArgumentException("Page doit être supérieur ou égal à 1", nameof(page));
        if (pageSize < 1 || pageSize > 100)
            throw new ArgumentException("PageSize doit être entre 1 et 100", nameof(pageSize));

        // Récupérer le nombre total et les demandes paginées
        var (total, requests) = await _invitationRequestRepository.GetByStatusAsync(status, entityTypeName, page, pageSize);

        // Mapper les entités en DTOs
        var requestDtos = _mapper.Map<List<InvitationRequestDto>>(requests);

        return new PagedResult<InvitationRequestDto>
        {
            Data = requestDtos,
            Page = page,
            PageSize = pageSize,
            Total = total
        };
    }
}