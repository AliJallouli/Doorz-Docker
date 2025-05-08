using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.UseCases.Register;
using BackEnd_TI.Utils;
using WebApi.Constants;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;
[Route("api/v1/register")]
[ApiController]
public class RegisterController:ControllerBase
{
     
    private readonly RegisterAdminFromInviteUseCase _registerAdminFromInviteUseCase;
    private readonly RegisterColleagueFromInviteUseCase _registerColleagueFromInviteUseCase;
    private readonly RegisterPublicUseCase _registerPublicUseCase;

    public RegisterController(
        RegisterPublicUseCase registerPublicUseCase,
        RegisterAdminFromInviteUseCase registerAdminFromInviteUseCase,
        RegisterColleagueFromInviteUseCase registerColleagueFromInviteUseCase)
    {
        
        _registerPublicUseCase = registerPublicUseCase ?? throw new ArgumentNullException(nameof(registerPublicUseCase));
        _registerAdminFromInviteUseCase = registerAdminFromInviteUseCase ??
                                         throw new ArgumentNullException(nameof(registerAdminFromInviteUseCase));
        _registerColleagueFromInviteUseCase = registerColleagueFromInviteUseCase ??
                                             throw new ArgumentNullException(
                                                 nameof(registerColleagueFromInviteUseCase));
        
    }
    
        /// <summary>
    ///     Inscrit un nouvel utilisateur public.
    /// </summary>
    /// <param name="publicRequest">Les données de l'utilisateur à inscrire.</param>
    /// <returns>Un objet contenant les informations d'authentification après inscription.</returns>
    /// <response code="200">Inscription réussie.</response>
    /// <response code="400">Requête invalide (données manquantes ou incorrectes).</response>
    [HttpPost("register-public")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterPublicRequestDto publicRequest)
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
        var response = await _registerPublicUseCase.ExecuteAsync(publicRequest, ipAddress, userAgent,languageCode);
        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.REGISTRATION_SUCCESS
        ));
    }

    /// <summary>
    ///     Inscrit un administrateur à partir d'une invitation.
    /// </summary>
    /// <param name="request">Les données de l'invitation et de l'utilisateur.</param>
    /// <returns>Un objet contenant les tokens d'accès et un message de succès.</returns>
    /// <response code="200">Inscription réussie.</response>
    /// <response code="400">Requête invalide ou invitation non valide.</response>
    /// <response code="500">Erreur serveur inattendue.</response>
    [HttpPost("register-admin-from-invite")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAdminFromInvite([FromBody] RegisterFromInviteRequestDto request)
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

        var response = await _registerAdminFromInviteUseCase.ExecuteAsync(request, ipAddress, userAgent, languageCode);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.REGISTRATION_SUCCESS
        ));
    }


    /// <summary>
    ///     Inscrit un collègue à partir d'une invitation.
    /// </summary>
    /// <param name="request">Les données de l'invitation et de l'utilisateur.</param>
    /// <returns>Un objet contenant les tokens d'accès et un message de succès.</returns>
    /// <response code="200">Inscription réussie.</response>
    /// <response code="400">Requête invalide ou invitation non valide.</response>
    /// <response code="500">Erreur serveur inattendue.</response>
    [HttpPost("register-colleague-from-invite")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterColleagueFromInvite([FromBody] RegisterFromInviteRequestDto request)
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
        var response = await _registerColleagueFromInviteUseCase.ExecuteAsync(request, ipAddress, userAgent, languageCode);
        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.REGISTRATION_SUCCESS
        ));
    }

}