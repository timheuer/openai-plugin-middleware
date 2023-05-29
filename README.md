[![TimHeuer.OpenAIPluginMiddleware](https://img.shields.io/nuget/v/TimHeuer.OpenAIPluginMiddleware.svg)](https://www.nuget.org/packages/TimHeuer.OpenAIPluginMiddleware)
[![GitHub Workflow Status (with branch)](https://img.shields.io/github/actions/workflow/status/timheuer/openai-plugin-middleware/release.yaml?branch=main)](https://github.com/timheuer/openai-plugin-middleware/actions/workflows/release.yaml)

# OpenAI Plugin manifest middleware for ASP.NET Core
This is a middleware for ASP.NET Core that will add a manifest file to the response for 
OpenAI Plugins in the default location of `/.well-known/ai-plugin.json`.

For more information on OpenAI Plugins see the [OpenAI Plugin documentation](https://platform.openai.com/docs/plugins/introduction).

## Installation
This is a NuGet package so if you are using Visual Studio just use the Package Manager and search for 'TimHeuer.OpenAIPluginMiddleware' (enable checking for pre-release) and install.

Additionally you can install the package using the .NET CLI if you are using other tools:

```bash
$ dotnet add package TimHeuer.OpenAIPluginMiddleware --version 1.0.10-pre
```

## Usage (pattern using ASP.NET minimal hosting/APIs)
Add the middleware to your `Program.cs` file (below is just an example, substitute your options):

```csharp
builder.Services.AddAiPluginGen(options =>
{
    options.NameForHuman = "Weather Forecast";
    options.NameForModel = "weatherforecast";
    options.LegalInfoUrl = "https://example.com/legal";
    options.ContactEmail = "noreply@example.com";
    options.LogoUrl = "https://example.com/logo.png";
    options.DescriptionForHuman = "Search for weather forecasts";
    options.DescriptionForModel = "Plugin for searching the weather forecast. Use It whenever a users asks about weather or forecasts";
    options.ApiDefinition = new Api() { RelativeUrl = "/swagger/v1/swagger.yaml" };
});
```

And then after the builder is built, add the middleware:

```csharp
app.UseAiPluginGen();
```

## Options
The options are mostly required to conform the plugin schema specification.  Some defaults are provided:
- Api/Type: "openapi"
- Api/IsUserAuthetnication: "false"
- Api/RelativeUrl: "/openapi.yaml"
- SchemaVersion: "v1"
- Auth/Type: "none"

#### Disclaimer
As with most of my projects this started as a learning, experiment, and selfish need.  It 
may not fit your needs at all.  That's okay, I'm not offended.  You are free to ignore 
it and move along. Or you are also free to provide some helpful feedback to make it better for yourself 
or others.
