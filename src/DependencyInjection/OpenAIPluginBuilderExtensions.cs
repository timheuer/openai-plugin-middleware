using OpenAIPluginMiddleware;

namespace Microsoft.AspNetCore.Builder;
public static class OpenAIPluginBuilderExtensions
{
    public static IApplicationBuilder UseAiPluginGen(this IApplicationBuilder app)
    {
        return app.UseMiddleware<OpenAiPluginMiddleware>();
    }
}
