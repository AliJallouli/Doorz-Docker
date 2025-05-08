
using System.Security.Claims;
using Application.UseCases.Auth.DTOs.EmailConfirmation;
using Application.UseCases.Auth.UseCases.EmailConfirmation;
using BackEnd_TI.Utils;
using WebApi.Constants;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[Route("api/v1/emailconfirmation")]
[ApiController]
public class EmailConfirmationController:ControllerBase
{
    private readonly ResendEmailConfirmationUseCase _resendEmailConfirmationUseCase;
    private readonly ConfirmEmaiUseCase _confirmEmaiUseCase;
    private readonly ValidateTokenForEmailConfirmationUseCase _validateTokenForEmailConfirmationUseCase;
    private readonly EmailConfirmedUseCase _emailConfirmedUseCase;
    private readonly ResendOtpCodeUseCase _resendOtpCodeUseCase;
    private   readonly ResendOtpCodeFromEmailUseCase _resendOtpCodeFromEmailUseCase;

    public EmailConfirmationController(
        ConfirmEmaiUseCase confirmEmaiUseCase,
        ResendEmailConfirmationUseCase resendEmailConfirmationUseCase,
        ValidateTokenForEmailConfirmationUseCase validateTokenForEmailConfirmationUseCase,
        EmailConfirmedUseCase emailConfirmedUseCase,
        ResendOtpCodeUseCase resendOtpCodeUseCase,
        ResendOtpCodeFromEmailUseCase resendOtpCodeFromEmailUseCase)
    {
        _confirmEmaiUseCase = confirmEmaiUseCase ?? throw new ArgumentNullException(nameof(confirmEmaiUseCase));
        _resendEmailConfirmationUseCase = resendEmailConfirmationUseCase;
        _validateTokenForEmailConfirmationUseCase = validateTokenForEmailConfirmationUseCase;
        _emailConfirmedUseCase = emailConfirmedUseCase;
        _resendOtpCodeUseCase = resendOtpCodeUseCase;
        _resendOtpCodeFromEmailUseCase = resendOtpCodeFromEmailUseCase;
    }
    
    [HttpPost("confirm-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequestDto request)
    {
        var result = await _confirmEmaiUseCase.ExecuteAsync(request);
        return Ok(ApiResponse<ConfirmEmailResponseDto>.Ok(result, result.Key));
    }

    [HttpPost("resend-email-confirmation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendEmailConfirmationRequestDto request)
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

        await _resendEmailConfirmationUseCase.ExecuteAsync(request, ipAddress, userAgent,languageCode);
        return Ok(ApiResponse<object>.Ok(
            null,
            "CONFIRMEMAIL.RESEND_SUCCESS"
        ));
    }
    [HttpPost("validate-email-comfirmation-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ValidateEmailConfirmationToken([FromBody] ConfirmEmailRequestDto request)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrEmpty(currentUserIdClaim))
        {
            await _emailConfirmedUseCase.ExecuteAsync(int.Parse(currentUserIdClaim));
        }

        var response = await _validateTokenForEmailConfirmationUseCase.ExecuteAsync(request);
        return Ok(ApiResponse<ConfirmEmailResponseDto>.Ok(response, response.Key));
    }
    
    [Authorize]
    [HttpPost("resend-confirmation_email_otp")]
    public async Task<IActionResult> ResendOtp()
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(currentUserIdClaim) || !int.TryParse(currentUserIdClaim, out var userId))
        {
            return Unauthorized(ApiResponse<object>.Fail(
                ResponseKeys.USER_NOT_AUTHENTICATED,
                null
            ));
        }
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
        var userAgent = Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);

        var metadata= await _resendOtpCodeUseCase.ExecuteAsync(userId,ipAddress, userAgent,languageCode);
        return Ok(ApiResponse<object>.Ok(
            metadata,
            "CONFIRMEMAIL.RESEND_SUCCESS"
        ));
    }
    
    [HttpPost("resend-otp-from-email")]
    public async Task<IActionResult> ResendOtpFromEmail([FromBody] ResendEmailConfirmationRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";
        var userAgent = Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);
        
        var metadata = await _resendOtpCodeFromEmailUseCase.ExecuteAsync(request.Email, ipAddress, userAgent, languageCode);

        return Ok(ApiResponse<object>.Ok(metadata, "CONFIRMEMAIL.RESEND_SUCCESS"));
    }




}