using AutoMapper;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles.Mapping.AutoMapper
{
    public class CustomerMapperProfile:Profile
    {
        public CustomerMapperProfile()
        {
            //ADD
            CreateMap<AddCustomerRequest, Customer>();
            CreateMap<Customer, AddCustomerResponse>();

            //UPDATE
            CreateMap<UpdateCustomerRequest, Customer>();
            CreateMap<Customer, UpdateCustomerResponse>();

            //DELETE
            CreateMap<Customer, DeleteCustomerResponse>();

            //GETBYID
            CreateMap<Customer,GetCustomerByIdResponse>();

            //GETLIST
            CreateMap<Customer, CustomerListItemDto>();
            CreateMap<IList<Customer>, GetCustomerListResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));


        }
    }
}
