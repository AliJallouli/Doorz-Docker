
using Application.UseCases.UsersSite.DTOs;
using Application.UseCases.Auth.DTOs;
using Application.UseCases.Invitation.Request.DTOs;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using Application.UseCases.Legals.DTOs;
using Application.UseCases.References.EntityType.DTOs;
using Application.UseCases.References.Language.DTOs;
using Application.UseCases.Support.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Legals;
using Domain.Entities.Support;
using Domain.Entities.References;

namespace Application;

public class Mapper : Profile
{
    public Mapper()
    {
        // Mapping pour Company -> CompanyDto
        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());

        // Mapping pour Institution -> InstitutionDto
        CreateMap<Institution, InstitutionDto>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        
        // Mapping pour Association -> AssociationDto
        CreateMap<Association, AssociationDto>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        
        // Mapping pour StudentMovement -> StudentMovementDto
        CreateMap<StudentMovement, StudentMovementDto>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        
        // Mapping pour StudentMovement -> StudentMovementDto
        CreateMap<PublicOrganization, PublicOrganizationDto>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());

        // Mapping pour Role -> RoleDto
        CreateMap<Role, RoleDto>();

     CreateMap<InvitationRequest, InvitationRequestDto>()
            .ForMember(dest => dest.InvitationRequestId, opt => opt.MapFrom(src => src.InvitationRequestId))
            .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(src => src.EntityTypeId))
            .ForMember(dest => dest.EntityTypeName, opt => opt.MapFrom(src => src.EntityType.Name)) // Mappe EntityType.Name
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.InvitationEmail, opt => opt.MapFrom(src => src.InvitationEmail))
            .ForMember(dest => dest.CompanyNumber, opt => opt.MapFrom(src => src.CompanyNumber))
            .ForMember(dest => dest.InstitutionTypeId, opt => opt.MapFrom(src => src.InstitutionTypeId))
            .ForMember(dest => dest.InstitutionTypeName, opt => opt.MapFrom(src => src.InstitutionType != null ? src.InstitutionType.Name : null)) // Mappe InstitutionType.Name si non null
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason))
            .ForMember(dest => dest.SubmittedIp, opt => opt.MapFrom(src => src.SubmittedIp))
            .ForMember(dest => dest.UserAgent, opt => opt.MapFrom(src => src.UserAgent))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("o")))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("o")));

        // Autres mappages existants
        CreateMap<InvitationRequest, CreateCompanyDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.InvitationEmail, opt => opt.MapFrom(src => src.InvitationEmail))
            .ForMember(dest => dest.CompanyNumber, opt => opt.MapFrom(src => src.CompanyNumber));


        CreateMap<InvitationRequest, CreateAssociationDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.InvitationEmail, opt => opt.MapFrom(src => src.InvitationEmail));
           

        CreateMap<InvitationRequest, CreateInstitutionDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.InvitationEmail, opt => opt.MapFrom(src => src.InvitationEmail))
            .ForMember(dest => dest.InstitutionTypeId, opt => opt.MapFrom(src => src.InstitutionTypeId));

        CreateMap<InvitationRequest, CreateStudentMovementDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.InvitationEmail, opt => opt.MapFrom(src => src.InvitationEmail));

        CreateMap<InvitationRequest, CreatePublicOrganizationDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.InvitationEmail, opt => opt.MapFrom(src => src.InvitationEmail));
    

        // Mapping de RegisterPublicRequestDto vers Users
        CreateMap<RegisterPublicRequestDto, Users>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.SuperRoleId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(_ => false));
        
        CreateMap<CreateInvitationRequestDto, InvitationRequest>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "PENDING"))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); 

        // Mapping pour Student
        CreateMap<RegisterPublicRequestDto, Student>()
            .ForMember(dest => dest.NotificationEnabled, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.ExpectedGraduationYear, opt => opt.MapFrom(_ => DateTime.Now.Year + 1))
            .ForMember(dest => dest.StudyField, opt => opt.MapFrom(_ => "Inconnu"));

        // Mapping pour Landlord
        CreateMap<RegisterPublicRequestDto, Landlord>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        // Mapping pour EntityUser
        CreateMap<RegisterPublicRequestDto, EntityUser>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.EntityId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<EntityType, EntityTypeDto>()
            .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(src => src.EntityTypeId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.NameTranslation, opt => opt.MapFrom(src =>
                src.Translations.Count > 0
                    ? src.Translations.First().TranslatedName
                    : src.Name
            ));


        // Mapping pour Institution types -> InstitutionTypeDto
        CreateMap<InstitutionType, InstitutionTypeDto>()
            .ForMember(dest => dest.InstitutionTypeId, opt => opt.MapFrom(src => src.InstitutionTypeId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


        CreateMap<Users, UserResponseDTO>()
            .ForMember(dest => dest.SuperRole, opt => opt.MapFrom(src => src.SuperRole.Name))
            .ForMember(dest => dest.Role,
                opt => opt.MapFrom(src => src.EntityUser != null ? src.EntityUser.Role.Name : null))
            .ForMember(dest => dest.EntityName,
                opt => opt.MapFrom(src => src.EntityUser != null ? src.EntityUser.Entity.Name : null))
            .ForMember(dest => dest.EntityType,
                opt => opt.MapFrom(src => src.EntityUser != null ? src.EntityUser.Entity.EntityType.Name : null))
            .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.IsVerified));


        CreateMap<ParsedUserAgentDto, UserAgent>()
            .ForMember(dest => dest.UserAgentValue, opt => opt.Ignore());
        CreateMap<LegalDocument, LegalDocumentDto>()
            .ForMember(dest => dest.DocumentTypeName, opt => opt.Ignore())
            .ForMember(dest => dest.DocumentTypeLabel, opt => opt.Ignore())
            .ForMember(dest => dest.LanguageCode, opt => opt.Ignore())
            .ForMember(dest => dest.Clauses, opt => opt.Ignore()); // car tu vas injecter les traductions manuellement

        CreateMap<LegalDocumentType, LegalDocumentTypeDto>()
            .ForMember(dest => dest.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Label, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.LanguageCode, opt => opt.Ignore())
            
          ;
        
        CreateMap<LegalDocumentClauseTranslation, LegalClauseDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.OrderIndex, opt => opt.MapFrom(src => src.Clause!.OrderIndex));

        CreateMap<LegalDocumentType, LegalDocumentTypeDto>()
            .AfterMap((src, dest) =>
            {
                var translation = src.Translations.FirstOrDefault(); 

                dest.Label = translation?.Name ?? src.Name;
                dest.Description = translation?.Description ?? "";
                dest.LanguageCode = translation?.Language.Code ?? "fr";
            });
        
        CreateMap<CreateContactMessageRequestDto, ContactMessage>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
            .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.ContactMessageTypeId, opt => opt.MapFrom(src => src.ContactMessageTypeId)) 
            .ForMember(dest => dest.IpAddress, opt => opt.Ignore())      
            .ForMember(dest => dest.UserAgentId, opt => opt.Ignore())      
            .ForMember(dest => dest.ReceivedAt, opt => opt.Ignore())       
            .ForMember(dest => dest.ContactMessageId, opt => opt.Ignore())
            .ForMember(dest => dest.Users, opt => opt.Ignore())
            .ForMember(dest => dest.UserAgent, opt => opt.Ignore())
            .ForMember(dest => dest.Language, opt => opt.Ignore());


        CreateMap<ContactMessageType, ContactMessageTypeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Translations.First().Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Translations.First().Description))
            .ForMember(dest => dest.ContactMessageTypeId, opt => opt.MapFrom(src => src.ContactMessageTypeId))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.KeyName, opt => opt.MapFrom(src => src.KeyName));

        CreateMap<SpokenLanguage, SpokenLanguageDto>()
            .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        
        CreateMap<SessionEvent, SessionDto>()
            .ForMember(dest => dest.UserAgent, opt => opt.MapFrom(src => src.UserAgent != null ? src.UserAgent.UserAgentValue : null));


    }
}