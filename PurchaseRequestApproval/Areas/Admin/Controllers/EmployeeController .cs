using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using PurchaseRequestApproval.Utility;

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
            // employee = _unitOfWork.Employee.Get(id.GetValueOrDefault()); // To add stored procedure

            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

            // var objFromDb = _unitOfWork.Employee.Get(id);

            employee = _unitOfWork.SP_Call.OneRecord<Employee>(SD.Proc_Employee_Get, parameter);




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
                var parameter = new DynamicParameters();
                parameter.Add("@EmployeeName", employee.EmployeeName);
                parameter.Add("@EmployeeCode", employee.EmployeePosition);
                parameter.Add("@EmployeePhone", employee.EmployeePhone);
                parameter.Add("@EmployeePosition", employee.EmployeePosition);
                parameter.Add("@EmployeeEmail", employee.EmployeeEmail);
                parameter.Add("@EmployeeSite", employee.EmployeeSite);


                if (employee.Id==0) // create case whenever no ID posted
                {
                    // _unitOfWork.Employee.Add(employee); // to allow sql procedrues
                    _unitOfWork.SP_Call.Execute(SD.Proc_Employee_Create, parameter);


                }
                else
                {
                    parameter.Add("@Id", employee.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Employee_Update, parameter);

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

            // var allObj = _unitOfWork.Employee.GetAll(); // commented to allow the link to procedures

            var allObj = _unitOfWork.SP_Call.List<Employee>(SD.Proc_Employee_GetAll, null); // to allow stored procedure
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
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

            // var objFromDb = _unitOfWork.Employee.Get(id);

            var objFromDb = _unitOfWork.SP_Call.OneRecord<Employee>(SD.Proc_Employee_Get, parameter);

            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            // _unitOfWork.Employee.Remove(objFromDb);

            _unitOfWork.SP_Call.Execute(SD.Proc_Employee_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
