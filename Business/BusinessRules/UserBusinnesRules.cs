using Core.CrossCuttingConcerns.Exceptions;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public class UserBusinnesRules
    {
        private readonly IUserDal _userDal;
        public UserBusinnesRules(IUserDal userDal)
        {
            _userDal = userDal;//İnjection
        }
        public void CheckIfUserNameExists(string name)
        {//İsim kontrolü-Daha önce bu isimde bir kayıt var mı?
            bool isNameExists = _userDal.Get(u => u.FirstName == name)!=null;
            if (isNameExists)
            {
                throw new BusinessException("User name already exists");
            }

        }
        public void CheckIfUserExists(User? user)
        {
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
        }
        

    }
}
