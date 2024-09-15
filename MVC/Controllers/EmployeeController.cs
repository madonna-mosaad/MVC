using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        //same comments as DepartmentController
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeRepository employeeRepository , IWebHostEnvironment webHostEnvironment) 
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string Name)
        {
            try
            {
                if (string.IsNullOrEmpty(Name))
                {
                    var employee = _employeeRepository.GetAll();
                    return View(employee);
                }
                var emp = _employeeRepository.GetByName(Name);
                return View(emp);
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    //log exception (the developer to handle the exception)
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    //friendly message to user
                    ModelState.AddModelError(string.Empty, "An Error occured during add department");
                }
                return View();
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee) 
        {

            if (ModelState.IsValid)
            {
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "successfully created";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "not successfully created";
                }
            }
            return View(employee);
        }
        public IActionResult Details(int? id,string ViewName="Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var employee= _employeeRepository.GetById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(ViewName,employee);
        }
        public IActionResult Delete(int id) 
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "error in delete");
                }
                return View(employee);
            }
        }
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Employee employee)
        {
            if(ModelState.IsValid) {
                if (employee.Id != id)
                { 
                return BadRequest();
                }
                try
                {
                    _employeeRepository.Update(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (_webHostEnvironment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "error in delete");
                    }
                }
            }
            return View(employee);
        }
    }
}
