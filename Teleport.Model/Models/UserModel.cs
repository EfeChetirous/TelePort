using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Teleport.Model.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string UserToken { get; set; }
    }

    public class UserRegisterModel
    {
        [Required(ErrorMessage = "UserName is a mandatory field!")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is a mandatory field!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is a mandatory field!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Password is a mandatory field!")]
        public string Password { get; set; }
    }
}
