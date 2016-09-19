using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Website.Models.DTO;

namespace Website.Models.BO
{
    public class AccountsBO
    {

        private string email;
        private string senha;

        public AccountsBO()
        {

        }

        public void doLogin(LoginViewModel login)
        {
            if(login != null)
            {
                string url = "http://ws.qoala.com.br/accounts/login";

                IEnumerable<KeyValuePair<string, string>> query = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("password", login.Password),
                    new KeyValuePair<string, string>("email", login.Email)
                };

                //new WSConn(url, query);
            }
            
        }

        public bool doRegister(IEnumerable<KeyValuePair<string, string>> user)
        {

            

            return true;
        }
        
    }
}