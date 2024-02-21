using AutoMapper;
using Business.Dtos.User;
using Business.Responses.Model;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.Mapping.AutoMapper
{
    public class UserMapperProfile:Profile
    {
        public UserMapperProfile()
        {
            CreateMap<AddUserRequest, User>();
            CreateMap<User, AddUserResponse>();

            CreateMap<User, UserListItemDto>();
            CreateMap<IList<User>, GetUserListResponse>()
                .ForMember(dest=>dest.Items, opt => opt.MapFrom(src => src));

            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserResponse>();

            CreateMap<User, DeleteUserResponse>();
            CreateMap<User, GetUserByIdResponse>();

        }
    }
}
