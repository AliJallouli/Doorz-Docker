using Application.UseCases.Invitation.SuperAdmin.DTOs;
using Domain.Exceptions;

namespace Application.Validation;

public static class InstitutionValidator
{
    public static void Validate(CreateInstitutionDto dto)
    {
        if (dto == null)
            throw new BusinessException(ErrorCodes.InstitutionDtoNull);

        CommonValidator.ValidateName(dto.Name);
        CommonValidator.ValidateEmail(dto.InvitationEmail);

        if (dto.InstitutionTypeId <= 0)
            throw new BusinessException(ErrorCodes.InstitutionTypeIdInvalid, "institutionTypeId");
    }
}