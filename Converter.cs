using Markdig;

namespace NoteSync;

public static class Converter
{
    public static string ConvertMdToHtml(string content)
    {
        return Markdown.ToHtml(content);
    }
}