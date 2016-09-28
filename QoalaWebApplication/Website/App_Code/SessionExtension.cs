using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Website.Models.ViewModels;

namespace System.Web.WebPages
{
    public static class SessionExtensions
    {
        public static UserViewModel CurrentUser(this HttpSessionStateBase obj)
        {
            return (UserViewModel)obj["CurrentUser"];
        }
    }
}