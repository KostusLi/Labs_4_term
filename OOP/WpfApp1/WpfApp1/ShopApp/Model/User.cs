using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ShopApp.Model
{
    public enum Role
    {
        Admin,
        Client
    }

    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }
        public User(string username, string password)
        {
            Id= Guid.NewGuid();
            this.Username = username;
            this.Password = password;
            Role = Role.Client;
        }

        public User()
        {
            this.Username = "Admin";
            this.Password = "sih";
            Role = Role.Admin;
        }
    }
}
