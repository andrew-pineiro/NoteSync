using System.Net.Http.Json;
using System.Text;

namespace NoteSync;
public class Jira
{
   private static string GetToken()
   {
      var secret = Config.JiraEmail + ":" + Config.JiraSecret;
      var bytes = Encoding.UTF8.GetBytes(secret);
      return Convert.ToBase64String(bytes);
   }
   private JiraReturnModel? GetPageByTitle(string title)
   {
      HttpSender sender = new();
      var results = sender.Send(GetToken(), "GET", "", Config.JiraBaseURL, $"/wiki/api/v2/pages?title={title}");
      var model = results.Content.ReadFromJsonAsync<JiraReturnModel>().Result;
      if (model != null)
      {
         if (model.results!.Count > 0)
         {
            return model;
         }
      }
      return null;
   }
   public void CreatePage(string parentId, string title, string content, out string createdId)
   {
      createdId = string.Empty;
      HttpSender sender = new();
      JiraModel model = new()
      {
         SpaceId = Config.JiraSpaceID,
         Title = title,
         ParentId = parentId,
         Body = new JiraBody()
         {
            //Default value for storing HTML in pages
            Representation = "storage",
            Value = Converter.ConvertMdToHtml(content)
         },
         Subtype = "",
         Version = new()
      };
      var results = sender.Send(GetToken(), "POST", model, Config.JiraBaseURL, $"/wiki/api/v2/pages");  
      if(results.Content.ReadAsStringAsync().Result.Contains("A page with this title already exists"))
      {
         var page = GetPageByTitle(title);
         if(page != null && !string.IsNullOrEmpty(page.results![0].id))
         {
            model.PageId = page.results[0].id;
            model.Version.number = Convert.ToInt32(page.results[0].version!.number) + 1;
            model.Version.message = "NoteSync Update";
            var subResults = sender.Send(GetToken(), "PUT", model, Config.JiraBaseURL, $"/wiki/api/v2/pages/{model.PageId}"); 
            if(subResults.IsSuccessStatusCode)
            {
               Console.WriteLine($"Updated page {model.PageId} - {title}");
            } else
            {
               Console.WriteLine($"ERROR UPDATING PAGE: {subResults.ReasonPhrase}");
               Console.WriteLine(subResults.Content.ReadAsStringAsync().Result);
            }
         }
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