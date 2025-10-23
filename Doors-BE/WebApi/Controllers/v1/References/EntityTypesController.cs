using Application.UseCases.References.EntityType.DTOs;
using Application.UseCases.References.EntityType.UseCase;
using WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1.References;

[Route("api/v1/entitytypes")]
[ApiController]
public class EntityTypesController : ControllerBase
{
    private readonly GetAllEntityTypesUseCase _getAllEntityTypesUseCase;

    public EntityTypesController(GetAllEntityTypesUseCase getAllEntityTypesUseCase)
    {
        _getAllEntityTypesUseCase = getAllEntityTypesUseCase;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);
        var entityTypesDtos = await _getAllEntityTypesUseCase.ExecuteAsync(languageCode);
        return Ok(ApiResponse<IEnumerable<EntityTypeDto>>.Ok(
            entityTypesDtos,
            "ENTITY_TYPES.RETRIEVED"
        ));
    }
}