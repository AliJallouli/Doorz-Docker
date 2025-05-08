using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(Users users);
    string GenerateRefreshToken();

    string GenerateEmailConfirmInviteToken();
    string GenerateSecurityToken();
    string GenerateOtpCode();
}