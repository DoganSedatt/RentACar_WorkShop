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
    public class EfCustomerDal : ICustomerDal
    {
        private readonly RentACarContext _context;
        public EfCustomerDal(RentACarContext carContext)
        {
            _context = carContext;
        }
        public Customer Add(Customer entity)
        {
            entity.CreatedAt = DateTime.Now;//Eklenme zamanı
            _context.Customers.Add(entity);//Customers tablosuna abone et
            _context.SaveChanges();//Değişiklikleri uygula
            return entity;//Bize o entityi dön
        }

        public Customer Delete(Customer entity, bool isSoftDelete = true)
        {
            entity.DeletedAt = DateTime.Now;
            _context.Customers.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public Customer? Get(Func<Customer, bool> predicate)
        {
            //predicate:Bana bir fonskiyon ver. Ben onu veritabanına gönderip,işleyip sonucu sana vereceğim
            Customer? customer=_context.Customers.FirstOrDefault(predicate);
            return customer;
        }

        public IList<Customer> GetList(Func<Customer, bool>? predicate = null)
        {
            IQueryable<Customer> query = _context.Set<Customer>();
            if (predicate != null)
            {
                query = query.Where(predicate).AsQueryable();
            }
            return query.ToList();
        }

        public Customer Update(Customer entity)
        {
            entity.UpdateAt = DateTime.Now;
            _context.Customers.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
