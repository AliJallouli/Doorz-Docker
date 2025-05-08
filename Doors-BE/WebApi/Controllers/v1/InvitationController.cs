using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.UseCases.Register;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/invitations")]
public class InvitationController : ControllerBase
{
    private readonly ILogger<InvitationController> _logger;
    private readonly ValidateInvitationTokenUseCase _validateInvitationTokenUseCase;

    public InvitationController(ValidateInvitationTokenUseCase validateInvitationTokenUseCase,
        ILogger<InvitationController> logger)
    {
        _validateInvitationTokenUseCase = validateInvitationTokenUseCase;
        _logger = logger;
    }

    [HttpGet("validate")]
    public async Task<ActionResult<ValidateInvitationTokenResponseDto>> Validate(
        [FromQuery] ValidateInvitationTokenRequestDto request)
    {
        _logger.LogInformation("Validating invitation token {InvitationToken}", request.InvitationToken);
        var result = await _validateInvitationTokenUseCase.ExecuteAsync(request);

        return Ok(ApiResponse<ValidateInvitationTokenResponseDto>.Ok(result, "INVITE.TOKEN.VALID"));
    }
}