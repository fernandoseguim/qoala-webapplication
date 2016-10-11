using Website.Models.ViewModels;

namespace System.Web.WebPages
{
    public static class SessionExtensions
    {
        public static UserViewModel CurrentUser(this HttpSessionStateBase obj)
        {
            return (UserViewModel)obj["CurrentUser"];
        }
        public static String CurrentUserName(this HttpSessionStateBase obj)
        {
            var user = ((UserViewModel)obj["CurrentUser"]);
            if (user != null)
                return user.Name;
            else
                return "";
        }
        public static string Token(this HttpSessionStateBase obj)
        {
            var token = obj["token"];
            if (token == null)
                return null;
            return token.ToString();
        }

        public static bool IsLogged(this HttpSessionStateBase obj)
        {
            var token = obj["token"];
            return token != null;
        }
    }
}