using Application.UseCases.References.EntityType.UseCase;
using WebApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1.References;

[Route("api/v1/institutiontypes")]
[ApiController]
public class InstitutionTypesController : ControllerBase
{
    private readonly GetAllInstitutionTypesUseCase _getAllInstitutionTypes;

    public InstitutionTypesController(GetAllInstitutionTypesUseCase getAllInstitutionTypes)
    {
        _getAllInstitutionTypes = getAllInstitutionTypes;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);
        var institutionTypeDtos = await _getAllInstitutionTypes.ExecuteAsync(languageCode);
        return Ok(institutionTypeDtos);
    }
}