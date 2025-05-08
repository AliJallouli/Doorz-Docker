using System.Net;
using Domain.Exceptions;
using WebApi.Contracts.Responses;

namespace WebApi.ExceptionHandlingMiddleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Continue le pipeline normalement
        }
        catch (BusinessException be)
        {
            // Pas de LogError : c’est une erreur métier normale
            context.Response.StatusCode = be.StatusCode;
            context.Response.ContentType = "application/json";
            _logger.LogInformation("BusinessException interceptée : {Key} (Champ : {Field})", be.Key, be.Field);

            // Réponse d'erreur sans traduction, en utilisant la clé d'erreur et le champ
            var response = ApiResponse<object>.Fail(
                be.Key,
                be.Field,
                be is IHasExtraData withExtra ? withExtra.ExtraData : null
            );

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            // Log uniquement les erreurs systèmes
            _logger.LogError(ex, "❌ Erreur système inattendue");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Réponse d'erreur générique
            var response = ApiResponse<object>.Fail(
                "InternalServerError",
                null
            );

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}