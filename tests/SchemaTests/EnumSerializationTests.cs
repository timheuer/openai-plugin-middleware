using System.Text.Json;

namespace SchemaTests;

public class EnumSerializationTests
{
    [Fact]
    public void Authentication_Enum_Values_Valid()
    {
        string json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthenticationType.ServiceHttp);
        Assert.Equal("\"service_http\"", json);

        json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthenticationType.UserHttp);
        Assert.Equal("\"user_http\"", json);

        json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthenticationType.None);
        Assert.Equal("\"none\"", json);

        json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthenticationType.OAuth);
        Assert.Equal("\"oauth\"", json);
    }

    [Fact]
    public void Authorization_Enum_Values_Valid()
    {
        string json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthorizationType.Bearer);
        Assert.Equal("\"bearer\"", json);

        json = JsonSerializer.Serialize(OpenAIPluginMiddleware.AuthorizationType.Basic);
        Assert.Equal("\"basic\"", json);
    }
}