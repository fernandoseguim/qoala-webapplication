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
    }
}