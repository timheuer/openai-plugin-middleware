using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OpenAIPluginMiddleware;
public static class AiPluginGenExtensions
{
    public static IServiceCollection AddAiPluginGen(this IServiceCollection services, Action<AiPluginOptions> config)
    {
        services.Configure(config);
        return services;
    }
    public static IServiceCollection AddAiPluginGen(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseAiPluginGen(this IApplicationBuilder app)
    {
        return app.UseMiddleware<OpenAiPluginMiddleware>();
    }
}
