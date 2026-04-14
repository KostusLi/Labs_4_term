using System;
using System.ComponentModel.DataAnnotations;

namespace WpfApp1.ShopApp.Model
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}