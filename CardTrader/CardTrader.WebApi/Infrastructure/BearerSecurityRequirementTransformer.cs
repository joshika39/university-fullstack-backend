using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CardTrader.WebApi.Infrastructure;

/// <summary>
/// Adds the JWT bearer security requirement to operations that require authorization.
/// </summary>
internal sealed class BearerSecurityRequirementTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider)
    : IOpenApiOperationTransformer
{
    private const string SchemeName = JwtBearerDefaults.AuthenticationScheme;

    /// <inheritdoc />
    public async Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        // Skip if JWT bearer auth is not configured
        if (await authenticationSchemeProvider.GetSchemeAsync(SchemeName) is null)
            return;

        // Check if the endpoint requires authorization (controller or action level)
        var metadata = context.Description.ActionDescriptor.EndpointMetadata;
        var requiresAuth = metadata.OfType<IAuthorizeData>().Any()
                        && !metadata.OfType<IAllowAnonymous>().Any();
        if (!requiresAuth)
            return;

        // Require the scheme for this endpoint
        operation.Security ??= [];
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(SchemeName, context.Document)] = []
        });
    }
}
