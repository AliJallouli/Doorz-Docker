using Domain.Entities;

namespace Application.UseCases.Auth.Service;

public interface IEmailAuthService
{
    Task SendConfirmationEmailLinkAsync(Users user, string confirmationToken, string codeOtp, string languageCode);
    Task SendOtpCodeEmailAsync(Users user, string codeOtp, string languageCode);
    Task SendPasswordReseLinkEmailAsync(Users user, string token, string otp, string ipAddress, string userAgent, string languageCode);
    Task SendPasswordChangedConfirmationEmailAsync(Users user, string ipAddress, string userAgent, string languageCode);
    Task SendEmailUpdatedConfirmationEmailAsync(Users user, string confirmationToken, string otpCode, string languageCode);

}
