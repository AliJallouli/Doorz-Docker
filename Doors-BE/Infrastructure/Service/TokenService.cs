using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Service;

public class TokenService : ITokenService
{
    private readonly string _audience; 
    private readonly string _issuer; 
    private readonly string _secretKey; 
    private readonly int _accessTokenExpirationMinutes;
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();


    public TokenService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException();
        _issuer = configuration["Jwt:Issuer"] ?? "doors-api";
        _audience = configuration["Jwt:Audience"] ?? "doors-clients";
        _accessTokenExpirationMinutes = configuration.GetValue("Jwt:AccessTokenExpirationMinutes", 15);
    }

    public string GenerateAccessToken(Users users,int sessionEventId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var userRole = users.SuperRole.Name;
        if (userRole == "Others")
            userRole = $"{users.EntityUser?.Entity.EntityType.Name}.{users.EntityUser?.Role.Name}";


        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, users.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, users.Email),
            new(ClaimTypes.Role, userRole),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("sid", sessionEventId.ToString())
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public string GenerateEmailConfirmInviteToken()
    {
        return Guid.NewGuid().ToString();
    }

    public string GenerateSecurityToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    public string GenerateOtpCode()
    {
        var buffer = new byte[4]; 
        _rng.GetBytes(buffer);
        int code = BitConverter.ToInt32(buffer, 0) & 0x7FFFFFFF; 
        
        code %= 1000000;
        
        return code.ToString("D6");
    }
}