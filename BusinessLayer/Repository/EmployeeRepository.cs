using BusinessLayer.Interfaces;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbcontext;
        public EmployeeRepository(AppDbContext dbContext) 
        { 
            _dbcontext = dbContext;
        }
        public int Add(Employee employee)
        {
            _dbcontext.Employees.Add(employee);
            return _dbcontext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _dbcontext.Employees.Remove(employee);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbcontext.Employees.AsNoTracking().ToList();
        }

        public Employee GetById(int id)
        {
            return _dbcontext.Employees.Find(id);
        }

        public int Update(Employee employee)
        {
            _dbcontext.Employees.Update(employee);
            return _dbcontext.SaveChanges();
        }
    }
}
