using System.Text.Json.Serialization;

namespace NoteSync;

public class JiraModel
{
    [JsonPropertyName("spaceId")]
    public required string? SpaceId { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; } = "current";
    [JsonPropertyName("title")]
    public required string? Title { get; set; }
    [JsonPropertyName("parentId")]
    public string? ParentId { get; set; }
    [JsonPropertyName("body")]
    public JiraBody? Body { get; set; }
    [JsonPropertyName("subtype")]
    public string? Subtype { get; set; } = string.Empty;
    [JsonPropertyName("id")]
    public string? PageId { get; set; }
    [JsonPropertyName("version")]
    public JiraVersion? Version { get; set; }
}
public class JiraBody
{
    [JsonPropertyName("representation")]
    public string? Representation { get; set; }
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

public class JiraReturnModel
{
    public List<Result>? results { get; set; }
    public JiraLinks? _links { get; set; }
}
public class Result
{
    public string? parentId { get; set; }
    public string? ownerId { get; set; }
    public string? lastOwnerId { get; set; }
    public DateTime createdAt { get; set; }
    public string? authorId { get; set; }
    public string? parentType { get; set; }
    public int position { get; set; }
    public JiraVersion? version { get; set; }
    public JiraBody? body { get; set; }
    public string? status { get; set; }
    public string? spaceId { get; set; }
    public string? title { get; set; }
    public string? id { get; set; }
    public JiraLinks? _links { get; set; }
}

public class JiraVersion
{
    public int number { get; set; }
    public string? message { get; set; }
    public bool minorEdit { get; set; }
    public string? authorId { get; set; }
    public DateTime createdAt { get; set; }
    public string? ncsStepVersion { get; set; }
}

public class JiraLinks
{
    public string? editui { get; set; }
    public string? webui { get; set; }
    public string? edituiv2 { get; set; }
    public string? tinyui { get; set; }
    public string? baseUrl { get; set; } // Added "baseUrl" for the root-level "_links.base"
}