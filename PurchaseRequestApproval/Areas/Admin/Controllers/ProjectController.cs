using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using PurchaseRequestApproval.Utility;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin_Modify + "," + SD.Role_Admin_View)]// Add authorization Level

    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectController(IUnitOfWork unitOfWork)
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
            Project project = new Project();
            if (id ==null) // create case
            {
                return View(project);

            }
            
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id


            // var objFromDb = _unitOfWork.Project.Get(id);

            project= _unitOfWork.SP_Call.OneRecord<Project>(SD.Proc_Project_Get, parameter);

            if (project == null) 
            { return NotFound(); }
            return View(project);
         //   return View();
        }

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Project project) 
        { 
            if (ModelState.IsValid)
            {
                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@ProjectName", project.ProjectName);
                parameter.Add("@WorkPackageIn", project.WorkPackageIn);
                parameter.Add("@ContractNo", project.ContractNo);
                

                if (project.Id==0) // create case whenever no ID posted
                {
                    //_unitOfWork.Project.Add(project);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Project_Create, parameter);


                }
                else
                {
                    //_unitOfWork.Project.Update(project);
                    parameter.Add("@Id", project.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Project_Update, parameter);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(project);
        
        
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            // var allObj = _unitOfWork.Project.GetAll();
            var allObj = _unitOfWork.SP_Call.List<Project>(SD.Proc_Project_GetAll, null); // to allow stored procedure
            return Json(new { data = allObj });


            


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id


            // var objFromDb = _unitOfWork.Project.Get(id);

            var objFromDb = _unitOfWork.SP_Call.OneRecord<Project>(SD.Proc_Project_Get, parameter);

            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            //_unitOfWork.Project.Remove(objFromDb);
            _unitOfWork.SP_Call.Execute(SD.Proc_Project_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save(); 
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
