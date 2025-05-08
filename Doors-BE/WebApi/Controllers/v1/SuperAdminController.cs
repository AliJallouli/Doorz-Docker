using System.Security.Claims;
using Application.UseCases.Invitation.SuperAdmin.DTOs;
using Application.UseCases.Invitation.SuperAdmin.UseCases;
using BackEnd_TI.Utils;
using WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/admin-invite")]
[Authorize(Policy = "SuperAdminOnly")]
public class SuperAdminController : ControllerBase
{
    private readonly AddCompanyUseCase _addCompanyUseCase;
    private readonly AddInstitutionUseCase _addInstitutionUseCase;

    public SuperAdminController(
        AddCompanyUseCase addCompanyUseCase,
        AddInstitutionUseCase addInstitutionUseCase)
    {
        _addCompanyUseCase = addCompanyUseCase ?? throw new ArgumentNullException(nameof(addCompanyUseCase));
        _addInstitutionUseCase =
            addInstitutionUseCase ?? throw new ArgumentNullException(nameof(addInstitutionUseCase));
    }

    /// <summary>
    ///     Invite un administrateur pour une nouvelle compagnie.
    ///     Réservé aux SuperAdmins via la politique "SuperAdminOnly".
    /// </summary>
    /// <param name="request">Les données de la compagnie à créer.</param>
    /// <returns>Un objet contenant les informations de la compagnie créée et un message de succès.</returns>
    /// <response code="200">Compagnie créée avec succès.</response>
    /// <response code="400">Requête invalide ou erreur lors de la création.</response>
    /// <response code="401">Utilisateur non authentifié.</response>
    /// <response code="403">Utilisateur authentifié mais pas SuperAdmin.</response>
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
                ResponseKeys.USER_NOT_AUTHENTICATED,
                null
            ));

        var currentUserId = int.Parse(currentUserIdClaim);
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);

        var response = await _addCompanyUseCase.ExecuteAsync(request, currentUserId, languageCode);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.COMPANY_ADMIN_INVITED
        ));
    }


    /// <summary>
    ///     Invite un administrateur pour une nouvelle institution.
    ///     Réservé aux SuperAdmins via la politique "SuperAdminOnly".
    /// </summary>
    /// <param name="request">Les données de l'institution à créer.</param>
    /// <returns>Un objet contenant les informations de l'institution créée et un message de succès.</returns>
    /// <response code="200">Institution créée avec succès.</response>
    /// <response code="400">Requête invalide ou erreur lors de la création.</response>
    /// <response code="401">Utilisateur non authentifié.</response>
    /// <response code="403">Utilisateur authentifié mais pas SuperAdmin.</response>
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
                ResponseKeys.USER_NOT_AUTHENTICATED,
                null
            ));

        var currentUserId = int.Parse(currentUserIdClaim);
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);

        var response = await _addInstitutionUseCase.ExecuteAsync(request, currentUserId, languageCode);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.INSTITUTION_ADMIN_INVITED
        ));
    }
}