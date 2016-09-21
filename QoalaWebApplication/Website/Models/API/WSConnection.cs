using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
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
        /// Resumo
        /// Este construtor instancia uma nova conexão com o Qoala WebService. <br/>
        /// 
        /// Parametros <br/>
        /// <paramref name="path"/> Recebe uma string com o caminho até o webservice. <br/>
        /// </summary>
        /// 
        /// <param name="path">Recebe uma string com o caminho até o webservice </param>
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
        /// Resumo
        /// Este metodo adiciona o cabeçalho à requisição
        /// </summary>
        public void AddHeader()
        {
            this._request.AddHeader("cache-control", "no-cache");
            this._request.AddHeader("content-type", "application/json");
        }

        /// <summary>
        /// 
        /// Resumo
        /// Este metodo adiciona um objeto JSON ao corpo da requisição
        /// 
        /// Parametros <br/>
        /// <paramref name="query"/> Recebe um IEnumerable de KeyValuePair<<string>stringKey</string>, <string>stringValue</string>> com a lista de parametros. <br/>
        /// </summary>
        /// 
        /// <param name="query">Recebe um IEnumerable de KeyValuePair<<string>stringKey</string>, <string>stringValue</string>> com a lista de parametros </param>
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
        /// Resumo
        /// Executa a requisição, obtendo devolvendo um JSON com resposta
        /// </summary>
        public string Execute()
        {
            IRestResponse response = _client.Execute(this._request);
            
            string content = response.Content;

            return content;
        }
    }
}