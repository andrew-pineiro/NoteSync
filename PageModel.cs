using System;
using System.Collections.Generic;

namespace NoteSync;
public class PageModel
{
    public List<Result>? Results { get; set; }
    public Links? Links { get; set; }
}

public class Result
{
    public string? Id { get; set; }
    public string? Status { get; set; }
    public string? Title { get; set; }
    public string? SpaceId { get; set; }
    public string? ParentId { get; set; }
    public string? ParentType { get; set; }
    public int Position { get; set; }
    public string? AuthorId { get; set; }
    public string? OwnerId { get; set; }
    public string? LastOwnerId { get; set; }
    public string? Subtype { get; set; }
    public string? CreatedAt { get; set; }
    public Version? Version { get; set; }
    public Body? Body { get; set; }
    public ResultLinks? Links { get; set; }
}

public class Version
{
    public string? CreatedAt { get; set; }
    public string? Message { get; set; }
    public int Number { get; set; }
    public bool MinorEdit { get; set; }
    public string? AuthorId { get; set; }
}

public class Body
{
    public Storage? Storage { get; set; }
    public AtlasDocFormat? AtlasDocFormat { get; set; }
}

public class Storage
{
    // Add properties as needed
}

public class AtlasDocFormat
{
    // Add properties as needed
}

public class ResultLinks
{
    public string? Webui { get; set; }
    public string? Editui { get; set; }
    public string? Tinyui { get; set; }
}

public class Links
{
    public string? Next { get; set; }
    public string? Base { get; set; }
}
