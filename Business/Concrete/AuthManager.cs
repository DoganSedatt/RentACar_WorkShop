using Azure.Core;
using Business.Abstract;
using Business.Requests.Auth;
using Core.Entities;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AccessToken = Core.Utilities.JWT.AccessToken;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IAuthDal _authDal;
        private readonly IRoleDal _roleDal;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IAuthDal authDal, IRoleDal roleDal,ITokenHelper tokenHelper)
        {
            _authDal = authDal;
            _roleDal = roleDal;
            _tokenHelper = tokenHelper;
        }

        
            
        public AccessToken Login(LoginRequest request)
        {
            Auth? auth = _authDal.Get(a => a.Email == request.Email);
            Role? role = _roleDal.Get(r => r.Id == request.RoleId);

            //İstekte yer alan Email db de var mı yok mu?
            bool isPasswordCorrect = HashingHelper.VerifyPassword(request.Password, auth.PasswordHash, auth.PasswordSalt);
            if (!isPasswordCorrect)
                throw new Exception("Şifre Yanlış");

            List<Claim> claims = new List<Claim>();
            new Claim(ClaimTypes.Email, auth.Email);
            new Claim(ClaimTypes.Role, auth.RoleId.ToString());
            return _tokenHelper.CreateToken(auth,claims);
        }

       

        public void Register(RegisterRequest request)
        {
            //Kayıt olma metodu
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(request.Password, out passwordHash, out passwordSalt);
            //Kayıt olurken verdiğim passworddan bir hash ve salt oluşturacak
            Role role = new Role();
            role.Id = request.RoleId;

            Auth auth = new Auth();
            auth.Email = request.Email;
            auth.RoleId = role.Id;
            auth.PasswordHash = passwordHash;
            auth.PasswordSalt = passwordSalt;
           
            
            _authDal.Add(auth);

        }


    }
}
