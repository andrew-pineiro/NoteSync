namespace NoteSync;

public class PageModel
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
    public string? CreatedAt { get; set; }
    public Body? Body { get; set; }
}
public class Body
{
    public object? Storage { get; set; }
    public object? Atlas_Doc_Format { get; set; }
    public object? View { get; set; }
}