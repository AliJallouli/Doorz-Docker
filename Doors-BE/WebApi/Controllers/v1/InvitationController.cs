using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.UseCases.Register;
using Application.UseCases.Invitation.Request.DTOs;
using Application.UseCases.Invitation.Request.UseCases;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Contracts.Responses;
using WebApi.Utils;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/invitations")]
public class InvitationController : ControllerBase
{
    private readonly ILogger<InvitationController> _logger;
    private readonly ValidateInvitationTokenUseCase _validateInvitationTokenUseCase;
    private readonly RequestInvitationRequestUseCase _requestUseCase;

    public InvitationController(ValidateInvitationTokenUseCase validateInvitationTokenUseCase,
        ILogger<InvitationController> logger,
        RequestInvitationRequestUseCase requestUseCase)
    {
        _validateInvitationTokenUseCase = validateInvitationTokenUseCase;
        _logger = logger;
        _requestUseCase = requestUseCase;
    }

    [HttpGet("validate")]
    public async Task<ActionResult<ValidateInvitationTokenResponseDto>> Validate(
        [FromQuery] ValidateInvitationTokenRequestDto request)
    {
        _logger.LogInformation("Validating invitation token {InvitationToken}", request.InvitationToken);
        var result = await _validateInvitationTokenUseCase.ExecuteAsync(request);

        return Ok(ApiResponse<ValidateInvitationTokenResponseDto>.Ok(result, "INVITE.TOKEN.VALID"));
    }
    
    [HttpPost("request")]
    public async Task<ActionResult<ApiResponse<string>>> SubmitInvitationRequest([FromBody] CreateInvitationRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail(
                ResponseKeys.InvalidModelState,
                null
            ));
        var languageCode= HttpContextUtils.ExtractLanguageCode(Request);
        var ipAddress = HttpContextUtils.ExtractClientIpAddress(Request);
        var userAgent = HttpContextUtils.ExtractUserAgent(Request);
        _logger.LogInformation("Réception d'une demande d'invitation via l'API pour {Email}", dto.InvitationEmail);

        await _requestUseCase.ExecuteAsync(dto, ipAddress, userAgent, languageCode);

        return Ok(ApiResponse<string>.Ok(null, "INVITATION_REQUEST.SUBMITTED"));
    }
}