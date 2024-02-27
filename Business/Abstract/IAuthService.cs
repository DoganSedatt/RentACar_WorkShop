using Azure.Core;
using Business.Requests.Auth;
using Core.Entities;
using Core.Utilities.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessToken = Core.Utilities.JWT.AccessToken;

namespace Business.Abstract
{
    public interface IAuthService
    {
        void Register(RegisterRequest request);
        //Kayıt imza metodu

        AccessToken Login(LoginRequest request);
        //Giriş imza metodu 
    }
}
