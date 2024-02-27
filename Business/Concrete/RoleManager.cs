using AutoMapper;
using Business.Abstract;
using Core.Entities;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal _roleDal;
        private readonly IMapper _mapper;

        public RoleManager(IRoleDal roleDal,IMapper mapper)
        {
            _roleDal = roleDal;
            _mapper = mapper;
        }

        public AddRoleResponse AddRole(AddRoleRequest request)
        {
            //1-Gelen isteği mapper kullanrak Role tipine çevir.
            var roleToAdded = _mapper.Map<Role>(request);
            //2-Çevrilen o tipi veritabanına ekle ve onu da bir Role değişkenine at
            Role addedRole=_roleDal.Add(roleToAdded);
            //3-O değişkeni de mapper ile AddRoleResponse'e çevirip var değişkenine at
            var response=_mapper.Map<AddRoleResponse>(addedRole);
            //4-return response yap
            return response;
        }
    }
}
