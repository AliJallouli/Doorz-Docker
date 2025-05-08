using System.Security.Claims;
using Application.UseCases.UsersSite.DTOs;
using Application.UseCases.UsersSite.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly GetUserByIdUseCase _getUserByIdUseCase;

    public UsersController(GetUserByIdUseCase getUserByIdUseCase)
    {
        _getUserByIdUseCase = getUserByIdUseCase;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<UserResponseDTO>>> GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized(ApiResponse<UserResponseDTO>.Fail("UNAUTHORIZED", "sub"));

        var userDto = await _getUserByIdUseCase.ExecuteAsync(userId);
        if (userDto == null) return NotFound(ApiResponse<UserResponseDTO>.Fail("USER_NOT_FOUND", "userId"));

        return Ok(ApiResponse<UserResponseDTO>.Ok(userDto, "USER_FETCH_SUCCESS"));
    }
}