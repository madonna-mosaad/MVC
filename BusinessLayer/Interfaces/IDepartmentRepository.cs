using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAll();
        public Department GetById(int id);
        public int Add(Department department);
        public int Update(Department department);
        public int Delete(Department department);

    }
}
