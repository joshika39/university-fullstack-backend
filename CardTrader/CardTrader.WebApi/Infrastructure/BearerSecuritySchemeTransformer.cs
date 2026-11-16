using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CardTrader.WebApi.Infrastructure;

/// <summary>
/// Adds the JWT bearer security scheme to the OpenAPI document.
/// </summary>
internal sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider)
    : IOpenApiDocumentTransformer
{
    private const string SchemeName = JwtBearerDefaults.AuthenticationScheme;

    /// <inheritdoc />
    public async Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        // Skip if JWT bearer auth is not configured
        if (await authenticationSchemeProvider.GetSchemeAsync(SchemeName) is null)
            return;

        // Create the containers if missing
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        // Define the scheme
        document.Components.SecuritySchemes[SchemeName] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Paste your JWT (without the 'Bearer ' prefix)"
        };
    }
}
