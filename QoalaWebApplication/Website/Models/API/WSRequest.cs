using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Text;

namespace Website.Models.API
{
    public class WSRequest
    {
#if DEBUG
        private static readonly string URLWebService = "http://localhost:52444/";
#else
        private static readonly string URLWebService = "http://ws.qoala.com.br/";
#endif

        private RestClient _client;
        private RestRequest _request;
        public Response response { get; set; }
        /// <summary>
        /// 
        /// <para>Resumo:  
        /// Este construtor instancia uma nova conexão com o Qoala WebService.</para>
        ///
        /// <para>
        /// Parametros: 
        /// <paramref name="path"/> Recebe uma string com o caminho até o webservice.</para>
        /// </summary>
        public WSRequest(string path)
        {
            string url = URLWebService + path;

            System.Console.WriteLine("WSRequest: " + url);

            this._client = new RestClient(url);

            this._request = new RestRequest();


            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "cache-control", "no-cache" },
                { "content-type", "application/json" }
            };

            AddHeaders(headers);
        }

        /// <summary>
        /// 
        /// <para>
        /// Resumo: 
        /// Este metodo adiciona o cabeçalho à requisição.</para>
        /// </summary>
        public void AddHeaders(Dictionary<string, string> dictonary)
        {
            foreach (KeyValuePair<string, string> dict in dictonary)
            {
                this._request.AddHeader(dict.Key, dict.Value);
            }
        }

        public void AddAuthorization(string token)
        {
            if (token != null)
            {
                Dictionary<string, string> headers = new Dictionary<string, string>()
                {
                    { "Authorization", " Token " + token },
                };
                AddHeaders(headers);
            }
        }

        /// <summary>
        /// 
        /// <para>
        /// Resumo: 
        /// Este metodo adiciona um objeto JSON ao corpo da requisição.</para>
        /// 
        /// <para>
        /// Parametros: 
        /// <paramref name="query"/> Recebe um IEnumerable da lista de parametros.</para>
        /// </summary>
        public void AddJsonParameter(IEnumerable<KeyValuePair<string, string>> query)
        {

            JsonObject json = new JsonObject();

            foreach (var item in query)
            {
                json.Add(item.Key, item.Value);
            }

            this._request.AddJsonBody(json);
        }

        /// <summary>
        /// 
        /// <para>Resumo: 
        /// Executa a requisição, obtendo devolvendo um JSON com resposta.</para> 
        /// </summary>
        public Response Execute()
        {
            IRestResponse response = _client.Execute(this._request);
            this.response = new Response(response.Content, (int)response.StatusCode);
            return this.response;
        }

        public Response Get()
        {
            this._request.Method = Method.GET;
            return Execute();
        }

        public Response Post()
        {
            this._request.Method = Method.POST;
            return Execute();
        }

        public Response Delete()
        {
            this._request.Method = Method.DELETE;
            return Execute();
        }

        public Response Put()
        {
            this._request.Method = Method.PUT;
            return Execute();
        }

        public class Response
        {
            public JObject Body { get; set; }
            public int Code { get; set; }

            public Response(string body, int code)
            {
                this.Body = JsonParser(body);
                this.Code = code;
            }

            public JObject JsonParser(string content)
            {
                return (JObject)JsonConvert.DeserializeObject(content);
            }
        }
    }
}