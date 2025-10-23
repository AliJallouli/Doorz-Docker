using System.Text.RegularExpressions;
using Application.UseCases.Invitation.Request.DTOs;
using Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Application.Validation;

public class InvitationRequestFormatValidator
{
    public static bool Validate(CreateInvitationRequestDto? dto, string entityTypeName)
    {
        if (dto == null)
            return false;

        if (!CommonFormatValidator.ValidateEntityName(dto.Name))
            return false;

        if (!CommonFormatValidator.ValidateEmail(dto.InvitationEmail))
            return false;

        if (entityTypeName == "Company")
        {
            if (string.IsNullOrWhiteSpace(dto.CompanyNumber))
                return false;

            if (!Regex.IsMatch(dto.CompanyNumber, @"^\d{4}\.\d{3}\.\d{3}$"))
                return false;
        }

        if (entityTypeName == "Institution")
        {
            if (dto.InstitutionTypeId is null or <= 0)
                return false;
        }

        return true;
    }
}