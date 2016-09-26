using System.Security.Principal;

namespace Website.Principal
{
    public class UserPrincipal : IUserPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public UserPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Permission { get; set; }
    }
}