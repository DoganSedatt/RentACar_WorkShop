using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer:Entity<int>
    {
        
        public int UserId {  get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
         

        public Customer()
        {
        }
        public Customer(string name,string lastName,int userId)
        {
           
            Name = name;
            LastName = lastName;
            UserId = userId;

        }
        public User? User { get; set; } = null;//one to one ilişki
    }
}
