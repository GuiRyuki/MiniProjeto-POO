using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjetoPOO
{
    public abstract class Usuario
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        protected string Password { get; private set; }

        protected Usuario(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }


    }
}
