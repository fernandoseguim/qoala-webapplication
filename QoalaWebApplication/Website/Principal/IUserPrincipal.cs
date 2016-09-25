using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Website.Principal
{
    interface IUserPrincipal : IPrincipal
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
    }
}