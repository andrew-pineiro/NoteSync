/*
curl -D- \
   -u <your_email@domain.com>:<your_user_api_token> \
   -X GET \
   -H "Content-Type: application/json" \
   https://<your-domain.atlassian.net>/wiki/rest/api/space
curl -D- \
   -X GET \
   -H "Authorization: Basic <your_encoded_string>" \
   -H "Content-Type: application/json" \
   "https://<your-domain.atlassian.net>/wiki/rest/api/space"

*/
using System.Text;

namespace NoteSync;
public class Jira
{
   public string GetToken()
   {
      //TODO: setup an appsettings file for secret
      var secret = File.ReadAllText("secret.txt");
      var bytes = Encoding.UTF8.GetBytes(secret);
      return Convert.ToBase64String(bytes);
   }
   public string GetBaseUrl()
   {
      //TODO: setup an appsettings file for base url
      var baseURL = File.ReadAllText("baseurl.txt");
      return baseURL;
    }
   public void GetPages()
   {
      string spaceId = "10321920";
      HttpSender sender = new();
      var results = sender.Send(GetToken(), "GET", "", GetBaseUrl(), $"/wiki/api/v2/pages?space-id={spaceId}");
      var content = results.Content.ReadAsStringAsync().Result;
      Console.WriteLine(content);
   }
   public void GetSpaces()
    {
      HttpSender sender = new();
      var results = sender.Send(GetToken(), "GET", "", GetBaseUrl(), "/wiki/api/v2/spaces");
      var content = results.Content.ReadAsStringAsync().Result;
      Console.WriteLine(content);

    }
   
}