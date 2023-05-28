using System.Collections.Specialized;
using System.Runtime.Serialization;
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
    [JsonIgnore]
    public string RelativeLogoUrl { get; set; } = "/logo.png";
    [JsonPropertyName("contact_email")]
    public string ContactEmail { get; set; }
    [JsonPropertyName("legal_info_url")]
    public string LegalInfoUrl { get; set; }
    [JsonIgnore]
    public string? BaseUri { get; set; }
}

public class Authentication
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public AuthenticationType Type { get; set; } = AuthenticationType.None;

    [JsonPropertyName("authorization_type")]
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public AuthorizationType? AuthorizationType { get; set; }

    [JsonPropertyName("authorization_url")]
    public string? AuthorizationUrl { get; set; }
    [JsonPropertyName("client_url")]
    public string? ClientUrl { get; set; }
    [JsonPropertyName("authorization_content_type")]
    public string? AuthorizationContentType { get; set; }
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }
    [JsonPropertyName("verification_tokens")]
    public Dictionary<string,string>? VerificationTokens { get; set; }
}

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum AuthenticationType
{
    [EnumMember(Value = "none")]
    None,
    [EnumMember(Value = "user_http")]
    UserHttp,
    [EnumMember(Value = "service_http")]
    ServiceHttp,
    [EnumMember(Value = "oauth")]
    OAuth 
}

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum AuthorizationType
{
    [EnumMember(Value = "bearer")]
    Bearer,
    [EnumMember(Value = "basic")]
    Basic
}

public class Api
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "openapi";
    [JsonPropertyName("is_user_authenticated")]
    public string IsUserAuthenticated { get; set; } = "false";
    [JsonPropertyName("url")]
    public string? Url { get;  set; }
    [JsonIgnore] 
    public string? RelativeUrl { get; set; } = "/openapi.yaml";
}
