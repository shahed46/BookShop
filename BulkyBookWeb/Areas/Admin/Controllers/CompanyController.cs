﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private object CoverTypeFromDb;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            
            return View();
        }
        

        //Edit controller
        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            
            
            if (id == null || id == 0)
            {
                //create company
                
                return View(company);
            }
            else
            {
                //update company
                company =_unitOfWork.Company.GetFirstOrDefault(u=>u.Id == id);
                return View(company);
            }
         
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
           
            if (ModelState.IsValid)
            {
                
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company Created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company updated successfully";
                }
                _unitOfWork.Save();
               
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Delete controller
        //GET
       
        

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList= _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }

        //POST
        [HttpDelete]
        
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success=false, message="error while deleting" });
            }

           

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "delete successful" });

            

        }
        #endregion
    }
}
