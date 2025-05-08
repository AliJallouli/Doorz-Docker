using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Application.Validation;


public static class CommonValidator
{
    public static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException(ErrorCodes.InvalidName, "name");
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new BusinessException(ErrorCodes.EmailEmpty, "email");

        var emailAttr = new EmailAddressAttribute();
        if (!emailAttr.IsValid(email))
            throw new BusinessException(ErrorCodes.InvalidEmailFormat, "email");

        if (email.Length < 4 || email.Length > 191)
            throw new BusinessException(ErrorCodes.EmailLengthOutOfRange, "email");
    }

    public static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new BusinessException(ErrorCodes.PasswordEmpty, "password");

        if (password.Length < 6 || password.Length > 100)
            throw new BusinessException(ErrorCodes.PasswordLengthInvalid, "password");

        if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
            throw new BusinessException(ErrorCodes.PasswordComplexityFailed, "password");
    }
}