using System.Text.Json.Serialization;

namespace NoteSync;

public class JiraModel
{
    [JsonPropertyName("spaceId")]
    public required string SpaceId { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; } = "current";
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("parentId")]
    public string? ParentId { get; set; }
    [JsonPropertyName("body")]
    public JiraBody? Body { get; set; }
    [JsonPropertyName("subtype")]
    public string Subtype { get; set; } = string.Empty;
}
public class JiraBody
{
    [JsonPropertyName("representation")]
    public string? Representation { get; set; }
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}
