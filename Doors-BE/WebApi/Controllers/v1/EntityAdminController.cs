using System.Security.Claims;
using Application.UseCases.Invitation.EntityAdmin.ColleagueInvitation.DTOs;
using Application.UseCases.Invitation.EntityAdmin.ColleagueInvitation.UseCase;
using BackEnd_TI.Utils;
using WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/collegue-invite")]
[Authorize(Policy = "AdminOnly")]
public class EntityAdminController : ControllerBase
{
    private readonly InviteColleagueUseCase _inviteColleagueUseCase;

    public EntityAdminController(InviteColleagueUseCase inviteColleagueUseCase)
    {
        _inviteColleagueUseCase =
            inviteColleagueUseCase ?? throw new ArgumentNullException(nameof(inviteColleagueUseCase));
    }

    /// <summary>
    ///     Invite un collègue à rejoindre une entité (compagnie ou institution).
    ///     Réservé aux utilisateurs ayant le rôle "Admin" via la politique "AdminOnly".
    /// </summary>
    /// <param name="request">Les données de l'invitation pour le collègue.</param>
    /// <param name="invitingUserId">L'ID de l'utilisateur qui envoie l'invitation.</param>
    /// <returns>Un objet contenant les détails de l'invitation et un message de succès.</returns>
    /// <response code="200">Invitation envoyée avec succès.</response>
    /// <response code="400">Requête invalide ou erreur lors de l'envoi de l'invitation.</response>
    /// <response code="401">Utilisateur non authentifié.</response>
    /// <response code="403">Utilisateur authentifié mais pas Admin.</response>
    [HttpPost("colleague-invitation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> InviteColleague([FromBody] InviteColleagueRequestDto request)
    {
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized(ApiResponse<object>.Fail(
                ResponseKeys.USER_NOT_AUTHENTICATED,
                null
            ));

        var currentUserId = int.Parse(currentUserIdClaim);
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);

        await _inviteColleagueUseCase.ExecuteAsync(request, currentUserId, languageCode);

        return Ok(ApiResponse<object>.Ok(
            null,
            ResponseKeys.COLLEAGUE_INVITED
        ));
    }
}