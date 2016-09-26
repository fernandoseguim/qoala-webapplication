namespace Website.Models
{
    public class User
    {
        public enum Profile { Publico = 1, Administrador = 2, Editor = 3 } 

        public string Name { get; set; }

        public string Email { get; set; }
    }
}