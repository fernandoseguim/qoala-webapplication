using Newtonsoft.Json;
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

        private string email;
        private string senha;

        public AccountsBO()
        {

        }

        public string doLogin(LoginViewModel model)
        {


            return null;
            
        }

        public bool doRegister(IEnumerable<KeyValuePair<string, string>> user)
        {
            
            return true;
        }
        
    }
}