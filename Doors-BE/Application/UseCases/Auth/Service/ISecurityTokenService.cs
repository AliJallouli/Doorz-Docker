using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.DTOs.Password;
using Domain.Entities;

namespace Application.UseCases.Auth.Service;

public interface ISecurityTokenService
{
    Task<GeneratedTokenResultDto> ReGenerateAndStoreAsync(int userId, string tokenTypeName, string ipAddress, string userAgent,
        string? deviceId = null, Dictionary<string, string>? metadata = null);

    Task<(SecurityToken,OtpRegenerationMetadata)> ValidateTokenAsync(string token, string expectedTypeName);

    Task MarkAsUsedAsync(SecurityToken token);


    Task RevokeAllForUserAsync(int userId, string tokenTypeName);


    Task<bool> IsTokenValidAsync(string token, string expectedTypeName);
    Task<IEnumerable<SecurityToken>> GetActiveTokensForUserAsync(int userId, string tokenTypeName);

    Task<GeneratedTokenResultDto> GenerateAndStoreAsync(
        int userId,
        string tokenTypeName,
        string ipAddress,
        string userAgent,
        string? deviceId = null,
        Dictionary<string, string>? metadata = null);

    Task<(SecurityToken,OtpRegenerationMetadata)> ValidateTokenAndOtpCodeAsync(string rawToken,string otpCode, string expectedTypeName);
    Task<SecurityToken?> FindByRawTokenAsync(string rawToken, string tokenTypeName);

    Task<(string rawOtp, OtpRegenerationMetadata meta)> RegenerateOtpOnlyAsync(int userId, string tokenTypeName, string ipAddress, string userAgent,
        string? deviceId = null);
}