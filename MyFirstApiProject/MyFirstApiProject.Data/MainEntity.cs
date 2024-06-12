using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstApiProject.Data
{
    public class MainEntity<T> : IDatahelper<T> where T : class
    {
        DBData dB;

        public MainEntity()
        {
            dB = new DBData();
        }

        public void Add(T entity)
        {
           if (entity != null)
            {
               dB.Set<T>().Add(entity);
                dB.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var user = Find(id);
            dB.Set<T>().Remove(user);
            dB.SaveChanges();
        }

        public T Find(int id)
        {
            return dB.Set<T>().Find(id);
        }

        public T Get(int id)
        {
            return dB.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return dB.Set<T>().ToList();
        }

        public void Update(T entity, int id)
        {
            dB.Set<T>().Update(entity);
        }
    }
}
