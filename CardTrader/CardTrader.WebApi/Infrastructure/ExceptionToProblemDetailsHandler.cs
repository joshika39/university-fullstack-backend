using System.ComponentModel.DataAnnotations;
using CardTrader.DataAccess.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CardTrader.WebApi.Infrastructure;

public class ExceptionToProblemDetailsHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ExceptionToProblemDetailsHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Map exception to status code
        int? statusCode = MapToStatusCode(exception);

        // Unmapped exception, pass it on
        if (statusCode is null)
            return false;

        // Set mapped status code on response
        httpContext.Response.StatusCode = statusCode.Value;

        // Build ProblemDetails, service fills in the defaults
        var problemDetails = new ProblemDetails
        {
            Detail = exception.Message,
        };

        // Write ProblemDetails to response
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }

    private static int? MapToStatusCode(Exception exception) => exception switch
    {
        // User exceptions
        InvalidCredentialsException or
        UserLockedOutException or
        InvalidRefreshTokenException =>
            StatusCodes.Status401Unauthorized,
        DuplicateUserException =>
            StatusCodes.Status409Conflict,

        // Common exceptions
        AccessDeniedException =>
            StatusCodes.Status403Forbidden,
        EntityNotFoundException =>
            StatusCodes.Status404NotFound,

        // Framework exceptions
        ValidationException =>
            StatusCodes.Status400BadRequest,

        _ => null
    };
}
