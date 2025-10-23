using System.Security.Claims;
using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.UseCases;
using Application.UseCases.UsersSite.DTOs;
using Application.UseCases.UsersSite.UseCase;
using WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly GetUserByIdUseCase _getUserByIdUseCase;
    private readonly UpdateUserNameUseCase _updateUserNameUseCase;

    public UsersController(GetUserByIdUseCase getUserByIdUseCase,
        UpdateUserNameUseCase updateUserNameUseCase)
    {
        _getUserByIdUseCase = getUserByIdUseCase;
        _updateUserNameUseCase = updateUserNameUseCase;
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
    
    [HttpPut("update-name")]
    [Authorize]
    public async Task<ActionResult> UpdateName([FromBody] UpdateNameRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail(ResponseKeys.InvalidModelState, null));

        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var sessionIdClaim = User.FindFirst("sid")?.Value;

        if (string.IsNullOrWhiteSpace(currentUserIdClaim) || !int.TryParse(currentUserIdClaim, out var userId) || string.IsNullOrEmpty(sessionIdClaim))
        {
            return Unauthorized(ApiResponse<object>.Fail(ResponseKeys.UserNotAuthenticated, null));
        }

        var sessionId = int.Parse(sessionIdClaim);
        
        var ipAddress = HttpContextUtils.ExtractClientIpAddress(Request);
        var userAgent = HttpContextUtils.ExtractUserAgent(Request);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var result = await _updateUserNameUseCase.ExecuteAsync(dto, userId,sessionId, ipAddress,userAgent, languageCode);
        return Ok(ApiResponse<object>.Ok(result, result.Key));
    }

}