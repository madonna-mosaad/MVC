using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
        private readonly IWebHostEnvironment _environment;//registered in AddControllerWithView as (Singelton) tomake developer log any exception 
        public DepartmentController(IDepartmentRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        //[HttpGet] is the default
        public IActionResult Index()
        {
            try
            {
                var department = _repository.GetAll();
                return View(department);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
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

        //[HttpGet] the default 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//to prevent any tool to arrive to this end-point(action)and edit any value of prop. of obj (parameter)=>(browser user only can)
                                  //above any action deal with DB and take parameter
        public IActionResult Create(Department newDepartment)
        {
            if (ModelState.IsValid)//mean that all validations (sever side or client side) is ok
            {
                try//to handle any exception appear in DB
                {
                    _repository.Add(newDepartment);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        //log exception (the developer to handle the exception)
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        //friendly message to user
                        ModelState.AddModelError(string.Empty, "An Error occured during add department");
                    }
                }
            }
            return View(newDepartment);//the reason of give the newDepartment as value of parameter that if Modelstate is not valid it will be in the same page with same data

        }

        //[HttpGet] the default 
        public IActionResult Details(int? id, string ViewName = "Details")//make int nullable to catch if the front not send the id value 
        {
            if (!id.HasValue)//if front doesnot send id (forget write asp-route-id="..")
            {
                return BadRequest();//400 state-Code (if front send wrong data or doesnot send any data ) 
            }
            Department dep = _repository.GetById(id.Value);
            if (dep == null)//if data not found in DB
            {
                return NotFound();// 404 state_Code (if the data not found in DB)
            }
            return View(ViewName, dep);
        }

        //[HttpGet] the default 
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");//to prevent code repeating
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//to prevent any tool to arrive to this end-point(action)and edit any value of prop. of obj (parameter)=>(browser user only can)
                                  //above any action deal with DB and take parameter
        public IActionResult Edit([FromRoute] int id, Department department)// I make id take its value from Route only to prevent any one to edit the id value from f12 code (in website) or any tools
        {
            if (ModelState.IsValid)//mean that all validations (sever side or client side) is ok
            {
                if (id != department.Id)//if any tool edit the department.Id then it will send badRequest
                {
                    return BadRequest();
                }
                try//to handle any exception appear in DB
                {
                    _repository.Update(department);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        //log exception (the developer to handle the exception)
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        //friendly message to user
                        ModelState.AddModelError(string.Empty, "An Error occured during edit department");
                    }
                }
            }
            return View(department);//the reason of give the newDepartment as value of parameter that if Modelstate is not valid it will be in the same page with same data

        }

        //[HttpGet] the default
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");//to prevent code repeating
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//to prevent any tool to arrive to this end-point(action) and edit any value of prop. of obj (parameter)=>(browser user only can)
                                  //above any action deal with DB and take parameter
        public IActionResult Delete(Department department)
        {
            try//to handle any exception appear in DB
            {
                _repository.Delete(department);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    //log exception (the developer to handle the exception)
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    //friendly message to user
                    ModelState.AddModelError(string.Empty, "An Error occured during edit department");
                }
                return View(department);//the reason of give the newDepartment as value of parameter that if Modelstate is not valid it will be in the same page with same data
            }
        }
    }
}