using Application.Configurations;
using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.UseCases.Register;
using WebApi.Utils;
using WebApi.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;
[Route("api/v1/register")]
[ApiController]
public class RegisterController:ControllerBase
{
     
    private readonly RegisterAdminFromInviteUseCase _registerAdminFromInviteUseCase;
    private readonly RegisterColleagueFromInviteUseCase _registerColleagueFromInviteUseCase;
    private readonly RegisterPublicUseCase _registerPublicUseCase;
    private readonly AuthSettings _authSettings;

    public RegisterController(
        RegisterPublicUseCase registerPublicUseCase,
        RegisterAdminFromInviteUseCase registerAdminFromInviteUseCase,
        RegisterColleagueFromInviteUseCase registerColleagueFromInviteUseCase,
        IOptions<AuthSettings> authSettings)
    {
        
        _registerPublicUseCase = registerPublicUseCase ?? throw new ArgumentNullException(nameof(registerPublicUseCase));
        _registerAdminFromInviteUseCase = registerAdminFromInviteUseCase ??
                                         throw new ArgumentNullException(nameof(registerAdminFromInviteUseCase));
        _registerColleagueFromInviteUseCase = registerColleagueFromInviteUseCase ??
                                             throw new ArgumentNullException(
                                                 nameof(registerColleagueFromInviteUseCase));
        _authSettings = authSettings.Value ?? throw new ArgumentNullException(nameof(authSettings));

        
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
                ResponseKeys.InvalidModelState,
                null
            ));

        
        var ipAddress = HttpContextUtils.ExtractClientIpAddress(Request);
        var userAgent = HttpContextUtils.ExtractUserAgent(Request);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);
        var response = await _registerPublicUseCase.ExecuteAsync(publicRequest, ipAddress, userAgent, languageCode);

        // Définir le cookie authCookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays( _authSettings.ShortLivedRefreshTokenDays),
            Path = "/"
        };

        HttpContext.Response.Cookies.Append("authCookie", response.RefreshToken, cookieOptions);

        return Ok(ApiResponse<object>.Ok(
            new
            {
                accessToken = response.AccessToken,
                refreshToken = response.RefreshToken
            },
            ResponseKeys.RegistrationSuccess
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
                ResponseKeys.InvalidModelState,
                null
            ));
        
        var ipAddress = HttpContextUtils.ExtractClientIpAddress(Request);
        var userAgent = HttpContextUtils.ExtractUserAgent(Request);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var response = await _registerAdminFromInviteUseCase.ExecuteAsync(request, ipAddress, userAgent, languageCode);

        // Définir le cookie authCookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays( _authSettings.ShortLivedRefreshTokenDays),
            Path = "/"
        };

        HttpContext.Response.Cookies.Append("authCookie", response.RefreshToken, cookieOptions);

        return Ok(ApiResponse<object>.Ok(
            new
            {
                accessToken = response.AccessToken,
                refreshToken = response.RefreshToken
            },
            ResponseKeys.RegistrationSuccess
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
                ResponseKeys.InvalidModelState,
                null
            ));
      
        var ipAddress = HttpContextUtils.ExtractClientIpAddress(Request);
        var userAgent = HttpContextUtils.ExtractUserAgent(Request);
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);
        var response = await _registerColleagueFromInviteUseCase.ExecuteAsync(request, ipAddress, userAgent, languageCode);
        // Définir le cookie authCookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays( _authSettings.ShortLivedRefreshTokenDays),
            Path = "/"
        };

        HttpContext.Response.Cookies.Append("authCookie", response.RefreshToken, cookieOptions);

        return Ok(ApiResponse<object>.Ok(
            new
            {
                accessToken = response.AccessToken,
                refreshToken = response.RefreshToken
            },
            ResponseKeys.RegistrationSuccess
        ));
    }

}