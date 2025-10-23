using Application.UseCases.References.EntityType.UseCase;
using WebApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.References;

[Route("api/v1/roles")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly GetRolesByEntityTypeNameUseCase _rolesByEntityTypeNameUseCase;

    public RoleController(GetRolesByEntityTypeNameUseCase rolesByEntityTypeNameUseCase)
    {
        _rolesByEntityTypeNameUseCase = rolesByEntityTypeNameUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetRolesByEntityTypeName([FromQuery] string entityTypeName)
    {
        if (string.IsNullOrWhiteSpace(entityTypeName)) return BadRequest("Entity type name is required.");

        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var rolesDtos = await _rolesByEntityTypeNameUseCase.ExecuteAsync(entityTypeName, languageCode);
        return Ok(rolesDtos);
    }
}