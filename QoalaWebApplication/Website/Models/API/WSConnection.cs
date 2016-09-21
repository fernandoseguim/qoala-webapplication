using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Text;

namespace Website.API
{
    public class WSConnection
    {

        private string _path;
        private StringBuilder _url;
        
        private RestClient _client;
        private RestRequest _request;

        /// <summary>
        /// 
        /// <para>Resumo:  
        /// Este construtor instancia uma nova conexão com o Qoala WebService.</para>
        ///
        /// <para>
        /// Parametros: 
        /// <paramref name="path"/> Recebe uma string com o caminho até o webservice.</para>
        /// </summary>
        public WSConnection(string path)
        {

            this._path = path;
            this._url = new StringBuilder().Append("http://ws.qoala.com.br/").Append(_path);

            string url = this._url.ToString();

            this._client = new RestClient(url);

            this._request = new RestRequest(Method.POST);

            AddHeader();
        }

        /// <summary>
        /// 
        /// <para>
        /// Resumo: 
        /// Este metodo adiciona o cabeçalho à requisição.</para>
        /// </summary>
        public void AddHeader()
        {
            this._request.AddHeader("cache-control", "no-cache");
            this._request.AddHeader("content-type", "application/json");
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
        public JObject Execute()
        {
            IRestResponse response = _client.Execute(this._request);
            
            string content = response.Content;

            return JsonParser(content);
        }


        public JObject JsonParser(string content)
        {
            return (JObject)JsonConvert.DeserializeObject(content);

        }
        
    }
}