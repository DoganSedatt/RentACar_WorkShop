using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.Mapping.AutoMapper
{
    public class RoleMapperProfiles:Profile
    {
        public RoleMapperProfiles()
        {
            CreateMap<AddRoleRequest, Role>();
            CreateMap<Role, AddRoleResponse>();
            
        }
    }
}
