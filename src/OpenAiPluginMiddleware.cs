﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace OpenAIPluginMiddleware;
public class OpenAiPluginMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AiPluginOptions _options;
    private readonly LinkGenerator _linkGenerator;
    private readonly EndpointDataSource _endpointDataSource;


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
            // help build the host if it is not set
            if (string.IsNullOrEmpty(_options.BaseUri))
            {
                var request = context.Request;
                var forwardedHost = request?.Headers["X-Forwarded-Host"].FirstOrDefault() ?? request?.Headers["Host"];
                var protocol = request?.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? request?.Scheme;
                _options.BaseUri = $"{protocol}://{forwardedHost}";
            }
            
            _options.ApiDefinition.Url = $"{_options.BaseUri}{_options.ApiDefinition.RelativeUrl}";

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, _options);
        }
        else
        {
            await _next(context);
        }
    }
}
