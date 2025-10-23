using Domain.Entities;

namespace Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(Users users, int sessionEventId);
    string GenerateRefreshToken();

    string GenerateEmailConfirmInviteToken();
    string GenerateSecurityToken();
    string GenerateOtpCode();
}