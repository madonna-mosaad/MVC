using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IGenericRepository <T> where T : ModelBase //ModelBase class or any class inherit the ModelBase to make sure that the class is Model(Table in DB)
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public int Add(T entity);
        public int Update(T entity);
        public int Delete(T entity);
    }
}
