using Application.UseCases.References.Language.DTOs;
using Application.UseCases.References.Language.UseCase;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1.References;
[Route("api/v1/languages")]
[ApiController]
public class SpokenLanguageController:ControllerBase
{
    private readonly GetAllSpokenLanguagesUseCase _useCase;

    public SpokenLanguageController(GetAllSpokenLanguagesUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTranslatedLanguages()
    {

        var result = await _useCase.ExecuteAsync();

        return Ok(ApiResponse<List<SpokenLanguageDto>>.Ok(result, "LANGUAGES.LOADED"));
        
    }
}