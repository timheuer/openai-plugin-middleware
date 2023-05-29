using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace OpenAIPluginMiddleware;

public class AiPluginOptions
{
    [JsonPropertyName("schema_version")]
    public string SchemaVersion { get; set; } = "v1";
    /// <summary>
    /// The model name cannot exceed 50 characters
    /// </summary>
    [JsonPropertyName("name_for_model")]
    public required string NameForModel { get; set; }

    /// <summary>
    /// The human name cannot exceed 20 characters
    /// </summary>
    [JsonPropertyName("name_for_human")]
    public required string NameForHuman { get; set; }
    
    /// <summary>
    /// The model description cannot exceed 8000 characters
    /// </summary>
    [JsonPropertyName("description_for_model")]
    public required string DescriptionForModel { get; set; }
    
    /// <summary>
    /// The human description cannot exceed 100 characters
    /// </summary>
    [JsonPropertyName("description_for_human")]
    public required string DescriptionForHuman { get; set; }
    [JsonPropertyName("auth")]
    public Authentication Auth { get; set; } = new();
    [JsonPropertyName("api")]
    public Api ApiDefinition { get; set; } = new();
    
    /// <summary>
    /// Suggested logo is a 512x512 png.
    /// </summary>
    [JsonPropertyName("logo_url")]
    public required string LogoUrl { get; set; }
    [JsonIgnore]
    public string RelativeLogoUrl { get; set; } = "/logo.png";
    [JsonPropertyName("contact_email")]
    public required string ContactEmail { get; set; }
    [JsonPropertyName("legal_info_url")]
    public required string LegalInfoUrl { get; set; }
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
