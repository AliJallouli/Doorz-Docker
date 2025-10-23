using Application.UseCases.Legals.DTOs;
using Application.UseCases.Legals.UseCases;
using WebApi.Constants;
using WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/legals")]
public class LegalDocumentsController:ControllerBase
{
    private readonly GetActiveLegalDocumentUseCase _getActiveLegalDocumentUseCase;
    private readonly GetAllLegalDocumentTypesUseCase _getAllLegalDocumentTypesUseCase;

    public LegalDocumentsController(GetActiveLegalDocumentUseCase useCase,
        GetAllLegalDocumentTypesUseCase getAllLegalDocumentTypesUseCase)
    {
        _getActiveLegalDocumentUseCase = useCase;
        _getAllLegalDocumentTypesUseCase = getAllLegalDocumentTypesUseCase;
    }

    [HttpGet("document/{documentTypeName}")]
    public async Task<IActionResult> GetByTypeAndLanguage(string documentTypeName)
    {
        if (string.IsNullOrWhiteSpace(documentTypeName))
        {
            return BadRequest(ApiResponse<object>.Fail(
                ResponseKeys.InvalidModelState,
                null
            ));
        }

        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var result = await _getActiveLegalDocumentUseCase.ExecuteAsync(documentTypeName, languageCode);

        return Ok(ApiResponse<LegalDocumentDto>.Ok(
            result,
            ResponseKeys.Success
        ));
    }

    [HttpGet("document-types")]
    public async Task<IActionResult> GetAllLegalDocumentTypes()
    {
        var languageCode = HttpContextUtils.ExtractLanguageCode(Request);

        var types = await _getAllLegalDocumentTypesUseCase.ExecuteAsync(languageCode);

       

        return Ok(ApiResponse<List<LegalDocumentTypeDto>>.Ok(
            types,
            ResponseKeys.Success
        ));
    }



    
}