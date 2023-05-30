namespace SchemaTests;

[TestClass]
public class EnumSerializationTests
{
    [TestMethod]
    public void Authentication_Enum_Values_Valid()
    {
        string json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthenticationType.ServiceHttp);
        Assert.AreEqual("\"service_http\"", json);

        json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthenticationType.UserHttp);
        Assert.AreEqual("\"user_http\"", json);

        json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthenticationType.None);
        Assert.AreEqual("\"none\"", json);

        json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthenticationType.OAuth);
        Assert.AreEqual("\"oauth\"", json);
    }

    [TestMethod]
    public void Authorization_Enum_Values_Valid()
    {
        string json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthorizationType.Bearer);
        Assert.AreEqual("\"bearer\"", json);

        json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthorizationType.Basic);
        Assert.AreEqual("\"basic\"", json);
    }
}