using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LordOfCodes.Console
{
    class Program
    {
        private static string Hastag = "universityofsalamanca";

        static void Main(string[] args)
        {
            GetInstagramData().Wait();
            System.Console.Read();
        }

        private static async Task GetInstagramData()
        {
            var appId = "4e9badaafa4e4436977733f01e05fbd0";
            Uri requestedUri = new Uri(string.Format("https://api.instagram.com/v1/tags/{0}/media/recent?client_id={1}", Hastag, appId), UriKind.Absolute);
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(requestedUri);
            var statusCode = response.StatusCode;
            string jsonData = await response.Content.ReadAsStringAsync();
            JObject parsed = JObject.Parse(jsonData);
            foreach (var pair in parsed)
            {
                System.Console.WriteLine((string.Format("{0}: {1}", pair.Key, pair.Value)));
            }
        }
    }
}
