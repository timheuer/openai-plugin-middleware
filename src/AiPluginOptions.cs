using System.Text.Json.Serialization;

namespace OpenAIPluginMiddleware;

public class AiPluginOptions
{
    [JsonPropertyName("schema_version")]
    public string SchemaVersion { get; set; } = "v1";
    [JsonPropertyName("name_for_model")]
    public string NameForModel { get; set; }
    [JsonPropertyName("name_for_human")]
    public string NameForHuman { get; set; }
    [JsonPropertyName("description_for_model")]
    public string DescriptionForModel { get; set; }
    [JsonPropertyName("description_for_human")]
    public string DescriptionForHuman { get; set; }
    [JsonPropertyName("auth")]
    public Authentication Auth { get; set; } = new();
    [JsonPropertyName("api")]
    public Api ApiDefinition { get; set; } = new();
    [JsonPropertyName("logo_url")]
    public string LogoUrl { get; set; }
    [JsonPropertyName("contact_email")]
    public string ContactEmail { get; set; }
    [JsonPropertyName("legal_info_url")]
    public string LegalInfoUrl { get; set; }

    [JsonIgnore]
    public string BaseUri { get; set; }
}

public class Authentication
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "none";
}

public class Api
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "openapi";
    [JsonPropertyName("is_user_authenticated")]
    public string IsUserAuthenticated { get; set; } = "false";
    [JsonPropertyName("url")]
    public string Url { get;  set; }
    [JsonIgnore] 
    public string RelativeUrl { get; set; } = "/openapi.yaml";
}
