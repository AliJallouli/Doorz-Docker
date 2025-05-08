
using Application.UseCases.Auth.DTOs.Password;
using Application.UseCases.Auth.UseCases.Password;
using BackEnd_TI.Utils;
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

    public PasswordController(
        SendPasswordResetLinkUseCase sendPasswordResetLinkUseCase,
        ValidatePasswordResetTokenUseCase validatePasswordResetTokenUseCase,
        ResetPasswordUseCase resetPasswordUseCase,
        ValidatePasswordResetOtpUseCase validatePasswordResetOtpUseCase)
    {
        
        _requestPasswordResetUseCase = sendPasswordResetLinkUseCase;
        _validatePasswordResetTokenUseCase = validatePasswordResetTokenUseCase;
        _resetPasswordUseCase = resetPasswordUseCase;
        _validatePasswordResetOtpUseCase = validatePasswordResetOtpUseCase;
    }
        [HttpPost("request-password-reset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail(
                ResponseKeys.INVALID_MODEL_STATE,
                null
            ));

        var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                        ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                        ?? "0.0.0.0";

        var userAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault()
                        ?? "Unknown";
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);

        await _requestPasswordResetUseCase.ExecuteAsync(request, ipAddress, userAgent,languageCode);

        return Ok(ApiResponse<object>.Ok(
            null,
            ResponseKeys.PASSWORD_RESET_EMAIL_SENT
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
                ResponseKeys.INVALID_MODEL_STATE,
                null
            ));

        var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                        ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                        ?? "0.0.0.0";

        var userAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault()
                        ?? "Unknown";
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);
        var result = await _resetPasswordUseCase.ExecuteAsync(request, ipAddress, userAgent, languageCode);

        return Ok(ApiResponse<ConfirmPasswordResetResponseDto>.Ok(
            result,
            result.Key
        ));
    }
}