using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Application.Validation;


public static class CommonFormatValidator
{
    public static bool ValidateNotNull<T>(T? dto) where T : class
    {
        return dto != null;
    }
    public static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException(ErrorCodes.InvalidName, "name");
    }

    public static bool ValidateEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var emailAttr = new EmailAddressAttribute();
        if (!emailAttr.IsValid(email))
            return false;

        if (email.Length < 4 || email.Length > 191)
            return false;
        return true;
    }

    public static bool ValidatePassword(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (password.Length < 6 || password.Length > 100)
            return false;

        if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
            return false;
        return true;
    }
    
    public static bool ValidateFirstName(string? firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return false;

        var trimmed = firstName.Trim();
        return trimmed.Length >= 2 && trimmed.Length <= 50;
    }

    public static bool ValidateLastName(string? lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return false;

        var trimmed = lastName.Trim();
        return trimmed.Length >= 2 && trimmed.Length <= 50;
    }
    
    public static bool ValidateEntityName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length >= 3 && name.Length <= 100;
    }

    public static bool ValidateCompanyNumber(string companyNumber)
    {
        if (string.IsNullOrEmpty(companyNumber))
            return false;

        if (!Regex.IsMatch(companyNumber, @"^\d{4}\.\d{3}\.\d{3}$"))
            return false;

        var cleanNumber = companyNumber.Replace(".", "");
        if (cleanNumber.Length != 10 || !cleanNumber.All(char.IsDigit))
            return false;

        var numberPart = cleanNumber.Substring(0, 8);
        var checkPart = cleanNumber.Substring(8, 2);

        if (!int.TryParse(numberPart, out var number))
            return false;

        if (!int.TryParse(checkPart, out var check))
            return false;

        //int expectedCheck = 97 - (number % 97);
        //if (expectedCheck == 0) expectedCheck = 97;

        //if (check != expectedCheck)
        //throw new BusinessException(ErrorCodes.COMPANY_CONTROL_NUMBER_INVALID, "companyNumber");
        return true;
    }
  
}