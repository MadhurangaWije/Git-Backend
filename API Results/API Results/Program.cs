using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API_Results
{
    class Program
    {
        static void Main(string[] args)
        {

            GetData().Wait();
        }
        public static async Task<string> GetData()
        {
            var url = "https://api.github.com/repos/dasaCoder/se_blog/stats/contributors";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0");
                Console.WriteLine(client.BaseAddress);
                HttpResponseMessage response = await client.GetAsync(url);
                string strResult = await response.Content.ReadAsStringAsync();
                JToken token = JArray.Parse(strResult);
                dynamic ResultArray = Newtonsoft.Json.JsonConvert.DeserializeObject(strResult);
                
                var contributor = ResultArray[0].author.login;
                List<CreateJson> jsonobj=new List<CreateJson> { };
                foreach (dynamic array in ResultArray) {
                    CreateJson a = new CreateJson();
                    a.total = array.total;
                    a.contributor = array.author.login;
                    jsonobj.Add(a);
                }
                foreach (CreateJson a in jsonobj)
                {
                    Console.WriteLine(a.total);
                    Console.WriteLine(a.contributor);
                }
                var json = JsonConvert.SerializeObject(jsonobj);
                Console.WriteLine(json);
                Console.WriteLine(contributor);
                return strResult;
            }
        }

    }
}
