using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        // create an method for upsert and can get null Id in case of create 
        public IActionResult Upsert(int? id)
        {
            Employee employee = new Employee();
            if (id ==null) // create case
            {
                return View(employee);

            }
            // this for edit request
            employee = _unitOfWork.Employee.Get(id.GetValueOrDefault());
            if (employee == null) 
            { return NotFound(); }
            return View(employee);
         //   return View();
        }

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Employee employee) 
        { 
            if (ModelState.IsValid)
            {
                if (employee.Id==0) // create case whenever no ID posted
                {
                    _unitOfWork.Employee.Add(employee);
                    

                }
                else
                {
                    _unitOfWork.Employee.Update(employee);
                    
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(employee);
        
        
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            var allObj = _unitOfWork.Employee.GetAll();
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.Employee.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Employee.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            _unitOfWork.Employee.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
