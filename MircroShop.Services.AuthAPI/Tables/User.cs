using Microsoft.AspNetCore.Identity;
using MircroShop.Services.AuthAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MircroShop.Services.AuthAPI.Tables
{
    public class User: IdentityUser
    {
        public User()
        {
            
        }

        public User(RegisterDto req)
        {
            Email = req.Email;
            UserName = req.Email;
            NormalizedEmail = req.Email.ToUpper();
            Name = req.Name;
            //PasswordHash = new PasswordHasher<User>().HashPassword(this, req.Password);
        }

        [StringLength(100)]
        public string Name { get; private set; } = string.Empty;
    }
}
