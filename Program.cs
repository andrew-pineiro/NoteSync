namespace NoteSync;

public class Program {
    private static void Main()
    {
        Jira jira = new();

        jira.GetPages();
        
    }    
}
    
    
    // const string ROOT_DIR = "/home/andrew/AndrewNotes";
// const string EXTEN = ".md";
// var files = Directory.GetFiles(ROOT_DIR, $"*{EXTEN}", SearchOption.AllDirectories);

// foreach (var file in files)
// {
//     string fileName = file.Substring(file.LastIndexOf('/') + 1);
//     string category = file.Replace(ROOT_DIR, "").Replace(fileName, "").Replace("/", "");
//     string subject = fileName.Replace(EXTEN, "");
//     Console.WriteLine(category + ": " + subject);
// }
