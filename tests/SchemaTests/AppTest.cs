using OpenAIPluginMiddleware;

namespace SchemaTests;

[TestClass]
public class AppTests
{
    const string WELLKNOWN_URI = "/.well-known/ai-plugin.json";
    HttpClient? client;
    AiPluginOptions? response;

    [TestInitialize]
    public async Task Initialize()
    {
        var factory = new SampleOpenAIPluginApp();
        client = factory.CreateClient();
        response = await client.GetFromJsonAsync<AiPluginOptions>(WELLKNOWN_URI);
    }

    [TestMethod]
    public void Has_Model_Name()
    {
        Assert.AreEqual("weatherforecast", response?.NameForModel);
    }

    [TestMethod]
    public void Has_Human_Name()
    {
        Assert.AreEqual("Weather Forecast", response?.NameForHuman);
    }

    [TestMethod]
    public void Has_Description_For_Model()
    {
        Assert.AreEqual("Plugin for searching the weather forecast. Use It whenever a users asks about weather or forecasts", response?.DescriptionForModel);
    }

    [TestMethod]
    public void Has_Legal_Info_Url()
    {
        Assert.IsNotNull(response?.LegalInfoUrl);
    }

    [TestMethod]
    public void Has_Contact_Email()
    {
        Assert.IsNotNull(response?.ContactEmail);
    }
    
    [TestMethod]
    public void DescriptionForHuman_Not_Too_Long()
    {
        Console.WriteLine(response?.DescriptionForHuman.Length.ToString());
        Assert.IsTrue(response?.DescriptionForHuman.Length <= 100);
    }

    [TestMethod]
    public void DescriptionForModel_Not_Too_Long()
    {
        Console.WriteLine(response?.DescriptionForModel.Length.ToString());
        Assert.IsTrue(response?.DescriptionForModel.Length <= 8000);
    }

    [TestMethod]
    public void NameForModel_Not_Too_Long()
    {
        Console.WriteLine(response?.NameForModel.Length.ToString());
        Assert.IsTrue(response?.NameForModel.Length <= 50);
    }

    [TestMethod]
    public void Has_Description_For_Human()
    {
        Assert.AreEqual("Search for weather forecasts", response?.DescriptionForHuman);
    }

    [TestMethod]
    public void Has_Logo_Url()
    {
        Console.WriteLine(response?.LogoUrl);
        Assert.IsNotNull(response?.LogoUrl);
    }

    [TestMethod]
    public void Has_Api_Definition_Url()
    {
        Console.WriteLine(response?.ApiDefinition.Url);
        Assert.IsNotNull(response?.ApiDefinition.Url);
    }

    [TestMethod]
    public async Task Api_Url_Is_Valid_Content()
    {
        var url = response?.ApiDefinition.Url;
        var apidef = await client?.GetAsync(url);
        var responseBody = await apidef.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
        Assert.IsTrue(apidef.IsSuccessStatusCode);
    }
}
