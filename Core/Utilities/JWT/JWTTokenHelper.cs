using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.JWT
{
    public class JWTTokenHelper:ITokenHelper
    {
        private IConfiguration _configuration;
        //Appsettings dosyasını okumak için bu interface'i kullanırız
        private TokenOptions _tokenOptions;
        //Appsetting'i okuyup içindeki değerleri bu sınıf içindeki değişkenlere eşitleyeceğiz
        public JWTTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //TokenOptions section'dan alınan bilgileri C# nesnesi olan TokenOptions'a dönüştürür
            
        }
       
        
        public AccessToken CreateToken(Auth auth,List<Claim> myClaims)
        {
            //ITokenHelper'ın CreateToken metodunu dolduruyoruz
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_tokenOptions.ExpirationTime);
            //Şimdiki saate _tokenOptions içinde yer alan ExpirationTime değerini ekle
            //Biz bu değer 10 vermişitk.Yani mevcut saate 10dk ekleyip token geçerlilik süresi belirleyecek
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: expirationTime,
                claims:myClaims,
                signingCredentials: signingCredentials
                
                );//Burada bir jwtToken oluşturduk.Appsettings içindeki değerleri kullanarak


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token=handler.WriteToken(jwtToken);

            return new AccessToken()
            {
                Token = token,
                ExpiretionTime = expirationTime,
                
            };
        }
    }
}
