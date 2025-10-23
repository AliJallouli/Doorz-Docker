using Application.UseCases.Auth.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.UseCases.Auth.Service;

public interface IRefreshTokenService
{
     Task<RefreshTokenResponseDto> CreateNewRefreshTokenTokenAsync(Users user, int sessionEventId,
        IDbContextTransaction transaction);
     
}