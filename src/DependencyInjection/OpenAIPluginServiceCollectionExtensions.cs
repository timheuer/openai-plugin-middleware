using OpenAIPluginMiddleware;

namespace Microsoft.Extensions.DependencyInjection;
public static class OpenAIPluginServiceCollectionExtensions
{
    public static IServiceCollection AddAiPluginGen(this IServiceCollection services, Action<AiPluginOptions> config)
    {
        if (config is null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        services.Configure(config);
        return services;
    }
}
