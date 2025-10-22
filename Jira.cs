using System.Net.Http.Json;
using System.Text;

namespace NoteSync;
public class Jira
{
   public string GetToken()
   {
      var secret = Config.JiraEmail + ":" + Config.JiraSecret;
      var bytes = Encoding.UTF8.GetBytes(secret);
      return Convert.ToBase64String(bytes);
   }
   public void CreatePage(string parentId, string title, string content, out string createdId)
   {
      createdId = string.Empty;
      HttpSender sender = new();
      JiraModel model = new()
      {
         SpaceId = Config.JiraSpaceID,
         Status = "current",
         Title = title,
         ParentId = parentId,
         Body = new JiraBody()
         {
            Representation = "storage",
            Value = content
         },
         Subtype = ""
      };
      var results = sender.Send(GetToken(), "POST", model, Config.JiraBaseURL, $"/wiki/api/v2/pages");     
      if(results.Content.ReadAsStringAsync().Result.Contains("A page with this title already exists"))
      {
         //TODO: handle comparing the contents to ensure they are up to date.
         return;
      }
      if(results.IsSuccessStatusCode)
      {
         var resultModel = results.Content.ReadFromJsonAsync<PageModel>().Result;
         createdId = resultModel!.Id!;
         Console.WriteLine($"Created page: {title} - {createdId}");      
      } else
      {
         Console.WriteLine($"ERROR CREATING PAGE: {results.ReasonPhrase}");
         Console.WriteLine($"{results.Content.ReadAsStringAsync().Result}");
      }
   }
}