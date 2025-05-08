using Application.UseCases.Auth.DTOs;
using Domain.Entities;

namespace Application.UseCases.Auth.Service;

public interface IAuthenticationService
{
    Task<AuthResponseDto> PerformAutoLoginAsync(Users user, string ipAddress, string successMessage, string userAgent);
    Task<int> ProcessUserAgentAsync(string userAgent);
}