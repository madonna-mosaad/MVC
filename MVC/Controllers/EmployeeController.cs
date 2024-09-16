using AutoMapper;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        //same comments as DepartmentController
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository , IWebHostEnvironment webHostEnvironment,IMapper mapper) 
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public IActionResult Index(string Name)
        {
            try
            {
                if (string.IsNullOrEmpty(Name))
                {
                    var employees = _employeeRepository.GetAll();
                    var mapped = _mapper.Map< IEnumerable<Employee> , IEnumerable<EmployeeViewModel> >(employees);
                    return View(mapped);
                }
                var emp = _employeeRepository.GetByName(Name);
                var mappedemp=_mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(emp);
                return View(mappedemp);
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
        public IActionResult Create(EmployeeViewModel employeeVM) 
        {

            if (ModelState.IsValid)
            {
                var mapped= _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                var count = _employeeRepository.Add(mapped);
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
            return View(employeeVM);
        }
        public IActionResult Details(int? id,string ViewName="Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var employee= _employeeRepository.GetById(id.Value);
            var mapped= _mapper.Map<Employee, EmployeeViewModel>(employee);
            if (employee == null)
            {
                return NotFound();
            }
            return View(ViewName,mapped);
        }
        public IActionResult Delete(int id) 
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel employeeVM)
        {
            try
            {
                var mapped = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                _employeeRepository.Delete(mapped);
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
                return View(employeeVM);
            }
        }
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,EmployeeViewModel employeeVM)
        {
            if(ModelState.IsValid) {
                if (employeeVM.Id != id)
                { 
                return BadRequest();
                }
                try
                {
                    var mapped = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _employeeRepository.Update(mapped);
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
            return View(employeeVM);
        }
    }
}
