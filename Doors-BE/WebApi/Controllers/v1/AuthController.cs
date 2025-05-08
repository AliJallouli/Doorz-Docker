

using Application.UseCases.Auth.DTOs;
using Application.UseCases.Auth.UseCases.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly GetRoleIdByRoleNameANdENtityNameUseCase _getRoleIdByRoleNameANdENtityNameUseCase;
    private readonly ILogger<AuthController> _logger;
    private readonly LoginUseCase _loginUseCase;
    private readonly LogoutUseCase _logoutUseCase;
    private readonly RefreshTokenUseCase _refreshTokenUseCase;

    public AuthController(
        LoginUseCase loginUseCase,
        RefreshTokenUseCase refreshTokenUseCase,
        LogoutUseCase logoutUseCase,
        ILogger<AuthController> logger,
        GetRoleIdByRoleNameANdENtityNameUseCase getRoleIdByRoleNameANdENtityNameUseCase)
    {
        _loginUseCase = loginUseCase ?? throw new ArgumentNullException(nameof(loginUseCase));
        _refreshTokenUseCase = refreshTokenUseCase ?? throw new ArgumentNullException(nameof(refreshTokenUseCase));
        _logoutUseCase = logoutUseCase ?? throw new ArgumentNullException(nameof(logoutUseCase));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _getRoleIdByRoleNameANdENtityNameUseCase = getRoleIdByRoleNameANdENtityNameUseCase;
    }



    /// <summary>
    ///     Authentifie un utilisateur et retourne des tokens d'accès.
    /// </summary>
    /// <param name="request">Les identifiants de l'utilisateur (email et mot de passe).</param>
    /// <returns>Un objet contenant les tokens d'accès et de rafraîchissement.</returns>
    /// <response code="200">Connexion réussie, cookie d'authentification défini.</response>
    /// <response code="400">Données invalides ou échec de la connexion.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
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
        var response = await _loginUseCase.ExecuteAsync(request, ipAddress, userAgent);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            Path = "/"
        };

        Response.Cookies.Append("authCookie", response.RefreshToken, cookieOptions);

        return Ok(ApiResponse<object>.Ok(
            new
            {
                accessToken = response.AccessToken,
                refreshToken = response.RefreshToken
            },
            ResponseKeys.LOGIN_SUCCESS
        ));
    }


    /// <summary>
    ///     Déconnecte un utilisateur en invalidant son refresh token.
    /// </summary>
    /// <param name="refreshToken">Le refresh token à invalider (requis).</param>
    /// <returns>Un message confirmant la déconnexion.</returns>
    /// <response code="200">Déconnexion réussie.</response>
    /// <response code="400">Erreur lors de la déconnexion (ex. token invalide).</response>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["authCookie"];
        if (string.IsNullOrWhiteSpace(refreshToken))
            return BadRequest(ApiResponse<object>.Fail("NO_REFRESH_TOKEN", "authCookie"));

        var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                        ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                        ?? "0.0.0.0";
        var userAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";

        var message = await _logoutUseCase.ExecuteAsync(refreshToken, ipAddress, userAgent);

        Response.Cookies.Delete("authCookie");

        return Ok(ApiResponse<object>.Ok(
            new { message },
            ResponseKeys.LOGOUT_SUCCESS
        ));
    }



    /// <summary>
    ///     Rafraîchit un token d'accès à partir d'un refresh token.
    /// </summary>
    /// <param name="refreshToken">Le refresh token à utiliser.</param>
    /// <returns>Un nouvel ensemble de tokens d'accès et de rafraîchissement.</returns>
    /// <response code="200">Rafraîchissement réussi.</response>
    /// <response code="400">Refresh token invalide ou expiré.</response>
    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["authCookie"];

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return BadRequest(ApiResponse<object>.Fail(
                ResponseKeys.INVALID_MODEL_STATE,
                "refreshToken"
            ));
        }

        var dto = new RefreshRequestDto { RefreshToken = refreshToken };

        var response = await _refreshTokenUseCase.ExecuteAsync(dto);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            Path = "/"
        };
        Response.Cookies.Append("authCookie", response.RefreshToken, cookieOptions);

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.REFRESH_SUCCESS
        ));
    }


    [HttpGet("check-session")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CheckSession()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return Ok(ApiResponse<object>.Ok(new { IsAuthenticated = true }, ResponseKeys.AUTHENTICATED));
        }
        return Ok(ApiResponse<object>.Ok(new { IsAuthenticated = false }, ResponseKeys.NOT_AUTHENTICATED));
    }



    /// <summary>
    ///     Teste l'authentification de l'utilisateur et retourne ses informations d'identité.
    ///     Cet endpoint est réservé aux utilisateurs ayant le rôle "SuperAdmin" selon la politique "SuperAdminOnly".
    /// </summary>
    /// <returns>Un objet JSON avec un message de succès et la liste des claims de l'utilisateur authentifié.</returns>
    /// <response code="200">Requête réussie, retourne les informations d'authentification.</response>
    /// <response code="401">Non authentifié (jeton manquant ou invalide).</response>
    /// <response code="403">Accès interdit (utilisateur authentifié mais pas SuperAdmin).</response>
    [HttpGet("test-auth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult TestAuth()
    {
        _logger.LogInformation("Test d'authentification exécuté. Utilisateur authentifié : {IsAuthenticated}",
            User.Identity?.IsAuthenticated);
        _logger.LogInformation("Claims : {Claims}", string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));
        _logger.LogInformation("Est SuperAdmin : {IsSuperAdmin}", User.IsInRole("SuperAdmin"));

        var response = new
        {
            message = "Succès",
            claims = User.Claims.Select(c => new { c.Type, c.Value }),
            isSuperAdmin = User.IsInRole("SuperAdmin"),
            isAuthenticated = User.Identity?.IsAuthenticated
        };

        return Ok(ApiResponse<object>.Ok(
            response,
            ResponseKeys.AUTH_TEST_SUCCESS
        ));
    }

    [HttpPost("get-role-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoleId([FromBody] RoleIdRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.RoleName) || string.IsNullOrWhiteSpace(request.EntityTypeName))
            return BadRequest(ApiResponse<object>.Fail("INVALID_ROLE_OR_ENTITY_TYPE", null));

        var roleIdResponseDto = await _getRoleIdByRoleNameANdENtityNameUseCase.ExecuteAsync(request);

        if (roleIdResponseDto.RoleId == 0) return NotFound(ApiResponse<object>.Fail("ROLE_NOT_FOUND", "RoleId"));

        return Ok(ApiResponse<object>.Ok(roleIdResponseDto, "ROLE_ID_FETCHED"));
    }
}