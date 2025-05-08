using Application.UseCases.References.EntityType.UseCase;
using BackEnd_TI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);
        var entityTypesDtos = await _getAllEntityTypesUseCase.ExecuteAsync(languageCode);
        return Ok(entityTypesDtos);
    }
}