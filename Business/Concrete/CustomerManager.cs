using AutoMapper;
using Business.Abstract;
using Business.BusinessRules;
using Business.Profiles.Validation.FluentValidation.Customer;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using System.Security.Claims;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly CustomerBusinnesRules _customerBusinnesRules;
        private readonly IAuthDal _authDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public CustomerManager(ICustomerDal customerDal, CustomerBusinnesRules customerBusinnesRules, IMapper mapper, IHttpContextAccessor httpContextAccessor, IAuthDal authDal)
        {
            _customerDal = customerDal;
            _mapper = mapper;
            _customerBusinnesRules = customerBusinnesRules;
            _httpContextAccesor = httpContextAccessor;
            _authDal = authDal;
        }


        public AddCustomerResponse Add(AddCustomerRequest request)
        {
            //Validation
            ValidationTool.Validate(new AddCustomerRequestValidation(), request);
            //Rules
            //_customerBusinnesRules.CheckIfCustomerNameExists(request.Name);

            //Mapping
            var customerToAdd = _mapper.Map<Customer>(request);
            //Dal-Add
            Customer addedCustomer = _customerDal.Add(customerToAdd);
            //MappingToResponse
            var response = _mapper.Map<AddCustomerResponse>(addedCustomer);
            return response;
        }

        public DeleteCustomerResponse Delete(DeleteCustomerRequest request)
        {
            //Veriye Eriş
            Customer? customer = _customerDal.Get(predicate: c => c.Id == request.Id);
            //Kontrolünü yap
            _customerBusinnesRules.CheckIfCustomerExists(customer);
            //Dal
            Customer customerToDelete = _customerDal.Delete(customer!);
            //Mapping
            var response = _mapper.Map<DeleteCustomerResponse>(customerToDelete);
            return response;
        }

        public GetCustomerByIdResponse GetById(GetCustomerByIdRequest request)
        {
            //Veriye Eriş
            Customer? customer = _customerDal.Get(predicate: c => c.UserId == request.Id);
            //Kontrolünü Yap
            _customerBusinnesRules.CheckIfCustomerExists(customer);
            //Mapping-Response
            var response = _mapper.Map<GetCustomerByIdResponse>(customer);
            return response;
        }


        public GetCustomerListResponse GetList(GetCustomerListRequest request)
        {
            

            if (!_httpContextAccesor.HttpContext.User.Identity.IsAuthenticated)
            {
                
                throw new Exception("Giriş yapmadan veri çekemezsiniz");
                

            }
            if (!_httpContextAccesor.HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin")) 
            {
                throw new Exception("Admin rolüne sahip olmalısınız");
            }
                //Verdiğim userId,customer içinde varsa listeye doldur.Yoksa tüm customer'ı getir
                IList<Customer> customerList = _customerDal.GetList(
            predicate: customer => request.FilterByUserId == null || customer.UserId == request.FilterByUserId
            );

            var response = _mapper.Map<GetCustomerListResponse>(customerList);

            return response;

        }

        public UpdateCustomerResponse Update(UpdateCustomerRequest request)
        {
            //Önce istek ile veritabanındaki id leri karşılaştırcak
            Customer? customerToUpdate = _customerDal.Get(predicate: c => c.Id == request.Id);
            //Buradan null bir sonuçta çıkabilir.Bunu da rules sınıfında kontrol ediyoruz
            _customerBusinnesRules.CheckIfCustomerExists(customerToUpdate);
            //Eğer yoksa hata verip programı durduracak
            //Eğer customer veritabanında varsa güncelleme işlemi için diğer kodlara gideceğiz.
            //Gelen isteği modelToUpdate'e çevir
            customerToUpdate = _mapper.Map(request, customerToUpdate);
            Customer updatedCustomer = _customerDal.Update(customerToUpdate);
            var response = _mapper.Map<UpdateCustomerResponse>(updatedCustomer);
            return response;

        }
    }
}
