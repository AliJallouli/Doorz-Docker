using System.Text.RegularExpressions;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using Domain.Exceptions;

namespace Application.Validation;


public static class CompanyValidator
{
    public static void Validate(CreateCompanyDto dto)
    {
        if (dto == null)
            throw new BusinessException(ErrorCodes.CompanyDtoNull);

        CommonValidator.ValidateName(dto.Name);
        CommonValidator.ValidateEmail(dto.InvitationEmail);
        ValidateCompanyNumber(dto.CompanyNumber);
    }

    public static void ValidateCompanyNumber(string companyNumber)
    {
        if (string.IsNullOrEmpty(companyNumber))
            throw new BusinessException(ErrorCodes.CompanyNumberRequired, "companyNumber");

        if (!Regex.IsMatch(companyNumber, @"^\d{4}\.\d{3}\.\d{3}$"))
            throw new BusinessException(ErrorCodes.CompanyNumberInvalidFormat, "companyNumber");

        var cleanNumber = companyNumber.Replace(".", "");
        if (cleanNumber.Length != 10 || !cleanNumber.All(char.IsDigit))
            throw new BusinessException(ErrorCodes.CompanyNumberInvalidDigits, "companyNumber");

        var numberPart = cleanNumber.Substring(0, 8);
        var checkPart = cleanNumber.Substring(8, 2);

        if (!int.TryParse(numberPart, out var number))
            throw new BusinessException(ErrorCodes.CompanyNumberInvalid, "companyNumber");

        if (!int.TryParse(checkPart, out var check))
            throw new BusinessException(ErrorCodes.CompanyNumberInvalid, "companyNumber");

        //int expectedCheck = 97 - (number % 97);
        //if (expectedCheck == 0) expectedCheck = 97;

        //if (check != expectedCheck)
        //throw new BusinessException(ErrorCodes.COMPANY_CONTROL_NUMBER_INVALID, "companyNumber");
    }
}