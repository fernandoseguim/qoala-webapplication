using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

using Website.API;

namespace Website.Models.BO
{
    public class AccountsBO
    {
        private static AccountsBO _account;
        //private string _token;

        private AccountsBO()
        {

        }

        public static AccountsBO Instance()
        {
            if (_account == null)
            {
                _account = new AccountsBO();
            }

            return _account;
        }

        public string doLogin(LoginViewModel model)
        {

            WSConnection conn = new WSConnection("accounts/login");

            IEnumerable<KeyValuePair<string, string>> login = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("email", model.Email)
                };

            conn.AddJsonParameter(login);

            var result = conn.Execute();
            string token;

            token = result.GetValue("token").ToString();

            return token;
            
        }

        public bool doRegister(IEnumerable<KeyValuePair<string, string>> user)
        {
            
            return true;
        }
        
    }
}