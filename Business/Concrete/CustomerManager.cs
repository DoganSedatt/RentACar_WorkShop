using AutoMapper;
using Business.Abstract;
using Business.BusinessRules;
using Business.Profiles.Validation.FluentValidation.Customer;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly CustomerBusinnesRules _customerBusinnesRules;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerDal customerDal,CustomerBusinnesRules customerBusinnesRules,IMapper mapper)
        {
            _customerDal = customerDal;
            _mapper = mapper;
            _customerBusinnesRules = customerBusinnesRules;
        }

        public AddCustomerResponse Add(AddCustomerRequest request)
        {
            //Validation
            ValidationTool.Validate(new AddCustomerRequestValidation(), request);
            //Rules
            _customerBusinnesRules.CheckIfCustomerNameExists(request.Name);

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
            var response=_mapper.Map<DeleteCustomerResponse>(customerToDelete);
            return response;
        }

        public GetCustomerByIdResponse GetById(GetCustomerByIdRequest request)
        {
            //Veriye Eriş
            Customer? customer = _customerDal.Get(predicate: c => c.Id == request.Id);
            //Kontrolünü Yap
            _customerBusinnesRules.CheckIfCustomerExists(customer);
            //Mapping-Response
            var response = _mapper.Map<GetCustomerByIdResponse>(customer);
            return response;
        }

        public GetCustomerListResponse GetList(GetCustomerListRequest request)
        {
            //Verdiğim ıd,customer içinde varsa listeye doldur.Yoksa listenin içi dolmayacak.
            IList<Customer> customerList = _customerDal.GetList(predicate:
                customer => request.FilterByUserId == null || customer.Id == request.FilterByUserId);

            var response=_mapper.Map<GetCustomerListResponse>(customerList);
            return response;
        }

        public UpdateCustomerResponse Update(UpdateCustomerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
