using Application.UseCases.Invitation.SuperAdmin.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("api/v1/companies")]
public class CompanyController : ControllerBase
{
    private readonly AddCompanyUseCase _addCompanyUseCase;

    public CompanyController(AddCompanyUseCase addCompanyUseCase)
    {
        _addCompanyUseCase = addCompanyUseCase;
    }
}