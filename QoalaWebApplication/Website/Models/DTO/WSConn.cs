using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;


namespace Website.Models.DTO
{
    public class WSConn
    {

        private string _url;

        public WSConn(string url, HttpContent query)
        {
            PostRequest(url, query);
        }

        async static void PostRequest(string url, HttpContent query)
        {
            
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url, query))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders headers = content.Headers;

                        Console.WriteLine(mycontent);
                    }
                }
            }

        }
        
    }
}