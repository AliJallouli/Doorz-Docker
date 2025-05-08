using Application.UseCases.References.EntityType.UseCase;
using BackEnd_TI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
        var languageCode = LanguageUtils.ExtractLanguageCode(Request);
        var institutionTypeDtos = await _getAllInstitutionTypes.ExecuteAsync(languageCode);
        return Ok(institutionTypeDtos);
    }
}