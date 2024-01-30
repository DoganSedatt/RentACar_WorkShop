﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer:Entity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public User? User { get; set; }//one to one ilişki 
    }
}