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