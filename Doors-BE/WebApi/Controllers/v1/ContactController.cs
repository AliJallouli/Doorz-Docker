using System.Security.Claims;
using Application.UseCases.Support.DTOs;
using Application.UseCases.Support.UseCases;
using BackEnd_TI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;
[ApiController]
[Route("api/v1/contact")]
public class ContactController:ControllerBase
{
    private readonly CreateContactMessageUseCase _createContactMessageUseCase;
    private readonly GetAllContactMessageTypesByLangUseCase _getAllContactMessageTypesByLangUseCase;

    public ContactController(CreateContactMessageUseCase createContactMessageUseCase,
        GetAllContactMessageTypesByLangUseCase getAllContactMessageTypesByLangUseCase)
    {
        _createContactMessageUseCase = createContactMessageUseCase;
        _getAllContactMessageTypesByLangUseCase = getAllContactMessageTypesByLangUseCase;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> PostMessage([FromBody] CreateContactMessageRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail(
                ResponseKeys.INVALID_MODEL_STATE,
                null
            ));
        }

        // 🛡 IP sécurisée (via reverse proxy éventuel ou fallback)
        var ipAddress = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                        ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                        ?? "0.0.0.0";

        // 🌐 User-Agent
        var userAgent = Request.Headers["User-Agent"].FirstOrDefault()
                        ?? "Unknown";

        // 👤 Utilisateur connecté ?
        if (User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                dto.UserId = userId;
            }
        }
        else
        {
            dto.UserId = null; 
        }

        await _createContactMessageUseCase.ExecuteAsync(dto,userAgent,ipAddress);

        return Ok(ApiResponse<object>.Ok(
            null,
            "CONTACT.MESSAGE_SENT_SUCCESS"
        ));
    }
    [HttpGet("message-types")]
    [AllowAnonymous]
    public async Task<IActionResult> GetMessageTypes()
    {
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);

        var types = await _getAllContactMessageTypesByLangUseCase.ExecuteAsync(languageCode);

        return Ok(ApiResponse<List<ContactMessageTypeDto>>.Ok(
            types,
            "CONTACT.MESSAGE_TYPES_RETRIEVED"
        ));
    }
}
