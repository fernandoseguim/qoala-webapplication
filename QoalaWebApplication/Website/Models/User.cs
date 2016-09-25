using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class User
    {
        public enum ePerfil { Publico = 1, Administrador = 2, Editor = 3 } 

        public string Nome { get; set; }

        public string Email { get; set; }
        
        public int Perfil { get; set; }

        public int newPassword { get; set; }
        

    }
}