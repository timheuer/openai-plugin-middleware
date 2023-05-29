using System.Diagnostics;
using System.Net.Http.Json;

namespace SchemaTests;
public class AppTests
{
    const string WELLKNOWN_URI = "/.well-known/ai-plugin.json";

    [Fact]
    public async Task Has_Model_Name()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.Equal("weatherforecast", response?.NameForModel);
    }

    [Fact]
    public async Task Has_Human_Name()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.Equal("Weather Forecast", response?.NameForHuman);
    }

    [Fact]
    public async Task Has_Description_For_Model()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.Equal("Plugin for searching the weather forecast. Use It whenever a users asks about weather or forecasts", response?.DescriptionForModel);
    }

    [Fact]
    public async Task Has_Legal_Info_Url()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.NotNull(response?.LegalInfoUrl);
    }

    [Fact]
    public async Task Has_Contact_Email()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.NotNull(response?.ContactEmail);
    }

    [Fact]
    public async Task Description_Not_Too_Long()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.True(response?.DescriptionForHuman.Length < 101);
    }

    [Fact]
    public async Task Has_Description_For_Human()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.Equal("Search for weather forecasts", response?.DescriptionForHuman);
    }

    [Fact]
    public async Task Has_Logo_Url()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Console.WriteLine(response?.LogoUrl);
        Assert.NotNull(response?.LogoUrl);
    }

    [Fact]
    public async Task Has_Api_Definition_Url()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Console.WriteLine(response?.ApiDefinition.Url);
        Assert.NotNull(response?.ApiDefinition.Url);
    }

    [Fact]
    public async Task Api_Url_Is_Valid_Content()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        var url = response?.ApiDefinition.Url;
        var apidef = await client.GetAsync(url);
        var responseBody = await apidef.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        Assert.True(apidef.IsSuccessStatusCode);
    }
}
