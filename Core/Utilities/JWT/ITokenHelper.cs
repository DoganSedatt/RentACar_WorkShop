using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.JWT
{
    public interface ITokenHelper
    {
        //Bana verdiği Auth'a bir accestoken üretip veriyor olacağım bu sınıf sayesinde
        //Access token içinde de token key'i ve geçerlilik süresi bulunuyor
        AccessToken CreateToken(Auth auth,List<Claim> claims);
    }
}
