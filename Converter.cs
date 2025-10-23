using Markdig;

namespace NoteSync;

public static class Converter
{
    // Uses MarkDig third-party package
    public static string ConvertMdToHtml(string content)
    {
        return Markdown.ToHtml(content);
    }
}