using Application.UseCases.Invitation.Service;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using AutoMapper;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Invitation.SuperAdmin.UseCases;

public class EntityInvitationStrategyFactory :IAddEntityStrategyResolver
{
     private readonly IServiceProvider _provider;
    private readonly IInvitationRequestRepository _invitationRequestRepository;
    private readonly IMapper _mapper;
    private readonly ISpokenLanguageRepository _spokenLanguageRepository;

    public EntityInvitationStrategyFactory(
        IServiceProvider provider,
        IInvitationRequestRepository invitationRequestRepository,
        IMapper mapper,ISpokenLanguageRepository spokenLanguageRepository)
    {
        _provider = provider;
        _invitationRequestRepository = invitationRequestRepository;
        _mapper = mapper;
        _spokenLanguageRepository = spokenLanguageRepository;
    }

    public async Task<object> ExecuteAsync(ProcessInvitationRequestDto processInvitationRequestDto, int createdBy)
    {
        var request = await _invitationRequestRepository.GetByIdAsync(processInvitationRequestDto.InvitationRequestId)
                      ?? throw new BusinessException(ErrorCodes.InvitationRequestNotFound);

        var languageCodeToProcess = (await _spokenLanguageRepository.GetByIdAsync(request.LanguageId!.Value)).Code;

            
        object result = request.EntityType.Name switch
        {
            "Company" => await _provider.GetRequiredService<IAddEntityUseCase<CreateCompanyDto, CompanyDto>>()
                .ExecuteAsync(_mapper.Map<CreateCompanyDto>(request), createdBy, languageCodeToProcess),

            "Association" => await _provider.GetRequiredService<IAddEntityUseCase<CreateAssociationDto, AssociationDto>>()
                .ExecuteAsync(_mapper.Map<CreateAssociationDto>(request), createdBy, languageCodeToProcess),

            "Institution" => await _provider.GetRequiredService<IAddEntityUseCase<CreateInstitutionDto, InstitutionDto>>()
                .ExecuteAsync(_mapper.Map<CreateInstitutionDto>(request), createdBy, languageCodeToProcess),

            "Public" => await _provider.GetRequiredService<IAddEntityUseCase<CreatePublicOrganizationDto, PublicOrganizationDto>>()
                .ExecuteAsync(_mapper.Map<CreatePublicOrganizationDto>(request), createdBy, languageCodeToProcess),

            "StudentMovement" => await _provider.GetRequiredService<IAddEntityUseCase<CreateStudentMovementDto, StudentMovementDto>>()
                .ExecuteAsync(_mapper.Map<CreateStudentMovementDto>(request), createdBy, languageCodeToProcess),

            _ => throw new BusinessException(ErrorCodes.UnsupportedEntityType)
        };
        

        await _invitationRequestRepository.UpdateStatusAsync(request.InvitationRequestId,InvitationRequestStatus.APPROVED);

        return result;
    }
}