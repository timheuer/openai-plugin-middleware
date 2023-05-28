using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace OpenAIPluginMiddleware;
public class OpenAiPluginMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AiPluginOptions _options;

    public OpenAiPluginMiddleware(RequestDelegate next,
                                  IOptions<AiPluginOptions> options)
    {
        _next = next;
        _options = options.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.Equals("/.well-known/ai-plugin.json"))
        {
            // help build the host if it is not set as base uri and no relative is set
            if (string.IsNullOrEmpty(_options.BaseUri) && !string.IsNullOrEmpty(_options.ApiDefinition.RelativeUrl))
            {
                var request = context.Request;
                var forwardedHost = request?.Headers["X-Forwarded-Host"].FirstOrDefault() ?? request?.Headers["Host"];
                var protocol = request?.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? request?.Scheme;
                _options.BaseUri = $"{protocol}://{forwardedHost}";
                _options.ApiDefinition.Url = $"{_options.BaseUri}{_options.ApiDefinition.RelativeUrl}";
            }

            // if the logo relative url is set use base 
            if (!string.IsNullOrEmpty(_options.RelativeLogoUrl)) _options.LogoUrl = $"{_options.BaseUri}{_options.RelativeLogoUrl}";

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, _options, new JsonSerializerOptions() {  DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull });
        }
        else
        {
            await _next(context);
        }
    }
}
