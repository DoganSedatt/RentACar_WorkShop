using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Auth:Entity<int>
    {
        //Genel auth fieldları
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        //public ICollection<Role>? Roles { get; set; }
        public int RoleId { get; set; }


    }
}
