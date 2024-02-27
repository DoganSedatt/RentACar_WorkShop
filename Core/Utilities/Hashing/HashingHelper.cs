using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Hashing
{
    public class HashingHelper
    {
        // Boilerplate Code=>Basmakalıp kodlar.Ezberlemeye kalkma
        //İlk olarak haslı ve saltlı bir password oluşturacak
        public static void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //Verilen şifrenin byte'larını çözerek hashla(HMACSHA512) ve passwordHash değişkenine at
            }
        }

        //Şifrenin hash ve saltını oluşturduk.Şimdi bunu doğrulayacağız
        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    //şifrenin hashli halini dışardan daha önce oluşmuş değerden alacak
                    //daha sonra bu fonk içinde mevcut şifremizi de tersine mühendislik ile Hashleyip byte olarak değişkene atıyoruz
                    if (passwordHash[i] != computedHash[i])
                        return false;//Daha sonra bu iki değeri karşılaştırıyoruz
                                     //True veya false bir değer dönecek
                }
                return true;
            }
        }
    }
}
