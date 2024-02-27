using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.JWT
{
    public class TokenOptions
    {
        //Appsettings içindeki TokenOptions değerlerini almak için bu sınıfı kullanacağız.
        //O değerleri tutan bir nesne olacak
        //Her seferinde tekrar tekrar gidip oradan okumak olmaz
        public string Issuer {  get; set; }
        public string Audience { get; set; }
        public int ExpirationTime { get; set; }
        public Claim Claims { get; set; }
        public string SecurityKey { get; set; }
        
    }
}
