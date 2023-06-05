namespace SchemaTests;

[TestClass]
public class AppTests
{
    const string WELLKNOWN_URI = "/.well-known/ai-plugin.json";

    [TestMethod]
    public async Task Has_Model_Name()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.AreEqual("weatherforecast", response?.NameForModel);
    }

    [TestMethod]
    public async Task Has_Human_Name()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.AreEqual("Weather Forecast", response?.NameForHuman);
    }

    [TestMethod]
    public async Task Has_Description_For_Model()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.AreEqual("Plugin for searching the weather forecast. Use It whenever a users asks about weather or forecasts", response?.DescriptionForModel);
    }

    [TestMethod]
    public async Task Has_Legal_Info_Url()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.IsNotNull(response?.LegalInfoUrl);
    }

    [TestMethod]
    public async Task Has_Contact_Email()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.IsNotNull(response?.ContactEmail);
    }
    
    [TestMethod]
    public async Task DescriptionForHuman_Not_Too_Long()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Console.WriteLine(response?.DescriptionForHuman.Length.ToString());
        Assert.IsTrue(response?.DescriptionForHuman.Length <= 100);
    }

    [TestMethod]
    public async Task DescriptionForModel_Not_Too_Long()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Console.WriteLine(response?.DescriptionForModel.Length.ToString());
        Assert.IsTrue(response?.DescriptionForModel.Length <= 8000);
    }

    [TestMethod]
    public async Task NameForModel_Not_Too_Long()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Console.WriteLine(response?.NameForModel.Length.ToString());
        Assert.IsTrue(response?.NameForModel.Length <= 50);
    }

    [TestMethod]
    public async Task Has_Description_For_Human()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Assert.AreEqual("Search for weather forecasts", response?.DescriptionForHuman);
    }

    [TestMethod]
    public async Task Has_Logo_Url()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Console.WriteLine(response?.LogoUrl);
        Assert.IsNotNull(response?.LogoUrl);
    }

    [TestMethod]
    public async Task Has_Api_Definition_Url()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        Console.WriteLine(response?.ApiDefinition.Url);
        Assert.IsNotNull(response?.ApiDefinition.Url);
    }

    [TestMethod]
    public async Task Api_Url_Is_Valid_Content()
    {
        var factory = new SampleOpenAIPluginApp();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<OpenAIPluginMiddleware.AiPluginOptions>(WELLKNOWN_URI);
        var url = response?.ApiDefinition.Url;
        var apidef = await client.GetAsync(url);
        var responseBody = await apidef.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        Assert.IsTrue(apidef.IsSuccessStatusCode);
    }
}
