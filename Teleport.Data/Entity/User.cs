using System;
using System.Collections.Generic;
using System.Text;
using Teleport.Data.Repository;

namespace Teleport.Data.Entity
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }        
        public string UserToken { get; set; }
    }
}
