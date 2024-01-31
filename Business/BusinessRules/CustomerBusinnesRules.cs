using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public class CustomerBusinnesRules
    {
        private readonly ICustomerDal _customerDal;
        public CustomerBusinnesRules(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public void CheckIfCustomerNameExists(string name)
        {
            bool isNameExists = _customerDal.Get(m => m.Name == name) != null;
            if (isNameExists)
                throw new BusinessException("Model name already exists.");
        }
        public void CheckIfCustomerExists(Customer? customer)
        {
            if (customer is null)
                throw new NotFoundException("Customer not found.");
        }
    }
}
