using System.Security.Claims;
using Application.UseCases.Invitation.Request.DTOs;
using Application.UseCases.Invitation.Request.UseCases;
using Application.UseCases.Invitation.Service;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using Application.UseCases.Invitation.SuperAdmin.UseCases;
using Domain.Enums;
using WebApi.Utils;
using WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;
using WebApi.Models;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/admin-invite")]
[Authorize(Policy = "SuperAdminOnly")]
public class SuperAdminController : ControllerBase
{
    private readonly IAddEntityStrategyResolver _strategyResolver;
    private readonly AddCompanyUseCase _addCompanyUseCase;
    private readonly AddInstitutionUseCase _addInstitutionUseCase;
    private readonly AddAssociationUseCase _addAssociationUseCase;
    private readonly AddStudentMovementUseCase _addStudentMovementUseCase;
    private readonly AddPublicOrganizationUseCase _addPublicOrganizationUseCase;
    private readonly GetInvitationRequestsByStatusUseCase _getInvitationRequestsByStatusUseCase;

    public SuperAdminController(
        IAddEntityStrategyResolver strategyResolver,
        AddCompanyUseCase addCompanyUseCase,
        AddInstitutionUseCase addInstitutionUseCase,
        AddAssociationUseCase addAssociationUseCase,
        AddStudentMovementUseCase addStudentMovementUseCase,
        AddPublicOrganizationUseCase addPublicOrganizationUseCase,
        GetInvitationRequestsByStatusUseCase getInvitationRequestsByStatus)
    {
        _strategyResolver = strategyResolver;
        _addCompanyUseCase = addCompanyUseCase ?? throw new ArgumentNullException(nameof(addCompanyUseCase));
        _addInstitutionUseCase =
            addInstitutionUseCase ?? throw new ArgumentNullException(nameof(addInstitutionUseCase));
        _addAssociationUseCase = addAssociationUseCase ?? throw new ArgumentNullException(nameof(addAssociationUseCase));
        _addStudentMovementUseCase = addStudentMovementUseCase ?? throw new ArgumentNullException(nameof(addStudentMovementUseCase));
        _addPublicOrganizationUseCase = addPublicOrganizationUseCase ?? throw new ArgumentNullException(nameof(addPublicOrganizationUseCase));
        _getInvitationRequestsByStatusUseCase = getInvitationRequestsByStatus;

    }

    
    [HttpPost("company")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> InviteCompanyAdmin([FromBody] CreateCompanyDto request)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized(ApiResponse<object>.Fail(
                ResponseKeys.UserNotAuthenticated,
                null
            ));

        var currentUserId = int.Parse(currentUserIdClaim);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var response = await _addCompanyUseCase.ExecuteAsync(request, currentUserId, languageCode);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.CompanyAdminInvited
        ));
    }



    [HttpPost("institution")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> InviteInstitutionAdmin([FromBody] CreateInstitutionDto request)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized(ApiResponse<object>.Fail(
                ResponseKeys.UserNotAuthenticated,
                null
            ));

        var currentUserId = int.Parse(currentUserIdClaim);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var response = await _addInstitutionUseCase.ExecuteAsync(request, currentUserId, languageCode);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.InstitutionAdminInvited
        ));
    }
    
    [HttpPost("association")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> InviteAssociationAdmin([FromBody] CreateAssociationDto request)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized(ApiResponse<object>.Fail(
                ResponseKeys.UserNotAuthenticated,
                null
            ));

        var currentUserId = int.Parse(currentUserIdClaim);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var response = await _addAssociationUseCase.ExecuteAsync(request, currentUserId, languageCode);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.AssociationAdminInvited
        ));
    }
    [HttpPost("student-movement")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> InviteStudentMovementAdmin([FromBody] CreateStudentMovementDto request)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized(ApiResponse<object>.Fail(
                ResponseKeys.UserNotAuthenticated,
                null
            ));

        var currentUserId = int.Parse(currentUserIdClaim);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var response = await _addStudentMovementUseCase.ExecuteAsync(request, currentUserId, languageCode);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.StudentMovementAdminInvited
        ));
    }

    [HttpPost("public-organization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> InvitePublicOrganizationAdmin([FromBody] CreatePublicOrganizationDto request)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized(ApiResponse<object>.Fail(
                ResponseKeys.UserNotAuthenticated,
                null
            ));

        var currentUserId = int.Parse(currentUserIdClaim);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var response = await _addPublicOrganizationUseCase.ExecuteAsync(request, currentUserId, languageCode);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.PublicOrganizationAdminInvited
        ));
    }
    
    [HttpGet("invitation-requests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetInvitationRequestsByStatus(
        [FromQuery] string? status = null,
        [FromQuery] string? entityTypeName = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized(ApiResponse<object>.Fail(
                ResponseKeys.UserNotAuthenticated,
                null
            ));

        InvitationRequestStatus? requestStatus = null;
        if (!string.IsNullOrEmpty(status))
        {
            if (!Enum.TryParse<InvitationRequestStatus>(status, true, out var parsedStatus))
            {
                return BadRequest(ApiResponse<object>.Fail(
                    "Statut invalide",
                    null
                ));
            }
            requestStatus = parsedStatus;
        }

        try
        {
            var languageCode = HttpContextUtils.ExtractLanguageCode(Request);
            var result = await _getInvitationRequestsByStatusUseCase.ExecuteAsync(
                requestStatus,
                entityTypeName,
                page,
                pageSize);

            return Ok(ApiResponse<PagedResult<InvitationRequestDto>>.Ok(
                result,
                "INVITATION_REQUESTS.RETRIEVED"
            ));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<object>.Fail(ex.Message, null));
        }
    }
    
    [HttpPost("from-request")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> InviteFromRequest([FromBody] ProcessInvitationRequestDto request)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized(ApiResponse<object>.Fail("NOT_AUTHENTICATED", null));

        var currentUserId = int.Parse(currentUserIdClaim);
        

        var result = await _strategyResolver.ExecuteAsync(request, currentUserId);

        return Ok(ApiResponse<object>.Ok(result, "INVITATION_REQUEST.ACCEPTED"));
    }



}