
using System.Security.Claims;
using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.DTOs.Password;
using Application.UseCases.Auth.UseCases.Password;
using WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using WebApi.Constants;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[Route("api/v1/password")]
[ApiController]
public class PasswordController:ControllerBase
{
    private readonly SendPasswordResetLinkUseCase _requestPasswordResetUseCase;
    private readonly ResetPasswordUseCase _resetPasswordUseCase;
    private readonly ValidatePasswordResetTokenUseCase _validatePasswordResetTokenUseCase;
    private readonly ValidatePasswordResetOtpUseCase _validatePasswordResetOtpUseCase;
    private readonly UpdatePasswordUseCase _updatePasswordUseCase;

    public PasswordController(
        SendPasswordResetLinkUseCase sendPasswordResetLinkUseCase,
        ValidatePasswordResetTokenUseCase validatePasswordResetTokenUseCase,
        ResetPasswordUseCase resetPasswordUseCase,
        ValidatePasswordResetOtpUseCase validatePasswordResetOtpUseCase,
        UpdatePasswordUseCase updatePasswordUseCas)
    {
        
        _requestPasswordResetUseCase = sendPasswordResetLinkUseCase;
        _validatePasswordResetTokenUseCase = validatePasswordResetTokenUseCase;
        _resetPasswordUseCase = resetPasswordUseCase;
        _validatePasswordResetOtpUseCase = validatePasswordResetOtpUseCase;
        _updatePasswordUseCase = updatePasswordUseCas;
    }
        [HttpPost("request-password-reset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail(
                ResponseKeys.InvalidModelState,
                null
            ));

       
        var ipAddress = HttpContextUtils.ExtractClientIpAddress(Request);
        var userAgent = HttpContextUtils.ExtractUserAgent(Request);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        await _requestPasswordResetUseCase.ExecuteAsync(request, ipAddress, userAgent,languageCode);

        return Ok(ApiResponse<object>.Ok(
            null,
            ResponseKeys.PasswordResetEmailSent
        ));
    }

    [HttpPost("validate-password-reset-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidatePasswordResetToken([FromBody] ValidatePasswordResetTokenRequestDto request)
    {
        var result = await _validatePasswordResetTokenUseCase.ExecuteAsync(request);
        return Ok(ApiResponse<ValidatePasswordResetTokenResponseDto>.Ok(result, result.Key));
    }
    
    [HttpPost("validate-password-reset-otp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidatePasswordResetOtp([FromBody] ValidatePasswordResetTokenAndOtpRequestDto request)
    {
        var result = await _validatePasswordResetOtpUseCase.ExecuteAsync(request);
        return Ok(ApiResponse<ValidatePasswordResetTokenResponseDto>.Ok(result, result.Key));
    }

    [HttpPost("confirm-reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmPasswordResetRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail(
                ResponseKeys.InvalidModelState,
                null
            ));

      
        var ipAddress = HttpContextUtils.ExtractClientIpAddress(Request);
        var userAgent = HttpContextUtils.ExtractUserAgent(Request);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);
        var result = await _resetPasswordUseCase.ExecuteAsync(request, ipAddress, userAgent, languageCode);

        return Ok(ApiResponse<ResponseWithSimplKeyDto>.Ok(
            result,
            result.Key
        ));
    }
    [HttpPut("update")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDto dto)
    {
        
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail(ResponseKeys.InvalidModelState,"modelState"));

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var sessionIdClaim = User.FindFirst("sid")?.Value;

        if (string.IsNullOrEmpty(sessionIdClaim))
            return Unauthorized(ApiResponse<object>.Fail(ResponseKeys.SessionNotFound, "sid"));

        var sessionId = int.Parse(sessionIdClaim);
      
        var ipAddress = HttpContextUtils.ExtractClientIpAddress(Request);
        var userAgent = HttpContextUtils.ExtractUserAgent(Request);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var result = await _updatePasswordUseCase.ExecuteAsync(dto, userId, sessionId, ipAddress, userAgent, languageCode);

        return Ok(ApiResponse<ResponseWithSimplKeyDto>.Ok(
            result,
            result.Key
        ));
    }

}