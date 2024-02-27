using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfRoleDal:EfEntityRepositoryBase<Role,int,RentACarContext>,IRoleDal
    {
        public EfRoleDal(RentACarContext context):base(context) { 
        }
        
    }
}
