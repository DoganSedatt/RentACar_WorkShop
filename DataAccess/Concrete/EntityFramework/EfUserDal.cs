using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : IUserDal
    {
        private readonly RentACarContext rentACarContext;
        public EfUserDal(RentACarContext _rentACarContext)
        {
            rentACarContext = _rentACarContext;
        }

        public User Add(User entity)
        {
            entity.CreatedAt= DateTime.Now;//Eklenme zamanı
            rentACarContext.Users.Add(entity);//Kullanıcılar tablosuna ekle
            rentACarContext.SaveChanges();//İşlemleri uygula
            return entity;//Bize bu entityi dön
        }

        public User Delete(User entity, bool isSoftDelete = true)
        {
            entity.DeletedAt= DateTime.Now;//Silinme zamanı
           // if(isSoftDelete)//Eğer softdelete false ise
                rentACarContext.Users.Remove(entity);//Entitiyi sil
            rentACarContext.SaveChanges();//Ayarları uygula
            return entity;
        }

        public User? Get(Func<User, bool> predicate)
        {
            User? user = rentACarContext.Users.FirstOrDefault(predicate);
            //bize user parametresi alan ve bool dönen bir fonksiyon verecek.
            //Tek bir user nesnesi dönecek
            return user;
        }

        public IList<User> GetList(Func<User, bool>? predicate = null)
        {
            IQueryable<User> query = rentACarContext.Set<User>();
            if (predicate != null)
            {
                query=query.Where(predicate).AsQueryable();
            }
            return query.ToList();
        }

        public User Update(User entity)
        {
            entity.UpdateAt= DateTime.Now;
            rentACarContext.Users.Update(entity);
            rentACarContext.SaveChanges();
            return entity;
        }
    }
}
