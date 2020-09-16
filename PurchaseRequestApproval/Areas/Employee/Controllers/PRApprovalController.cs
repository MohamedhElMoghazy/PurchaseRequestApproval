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
using Microsoft.AspNetCore.Mvc.Rendering;
using PurchaseRequestApproval.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = SD.Role_Admin_Modify + "," + SD.Role_Admin_View + "," + SD.Role_Employee_Modify + "," + SD.Role_Employee_View)]// Add authorization Level

    public class PRApprovalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PRApprovalController(IUnitOfWork unitOfWork)
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
            PRApprovalVM prapprovalVM = new PRApprovalVM() 
            {
                PRApproval = new PRApproval(),
                EmployeeList = _unitOfWork.Employee.GetAll().Select(i => new SelectListItem
                {
                    Text = i.EmployeeName,
                    Value = i.Id.ToString()

                }),

                VendorList = _unitOfWork.Vendor.GetAll().Select(i => new SelectListItem
                {
                    Text = i.VendorName,
                    Value = i.Id.ToString()

                }),

                PurchaseTypeList = _unitOfWork.PurchaseType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.PurcahseTypeName,
                    Value = i.Id.ToString()

                }),

                ProjectList = _unitOfWork.Project.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ProjectName,
                    Value = i.Id.ToString()

                }),






            };
            if (id ==null) // create case
            {
                return View(prapprovalVM);

            }
            // this for edit request
            prapprovalVM.PRApproval = _unitOfWork.PRApproval.Get(id.GetValueOrDefault()); // To add stored procedure

           // var parameter = new DynamicParameters(); // arrange parameters for sql server
            //parameter.Add("@Id", id); // arrange to send the Id

            // var objFromDb = _unitOfWork.PRApproval.Get(id);

            //prapproval = _unitOfWork.SP_Call.OneRecord<PRApproval>(SD.Proc_PRApproval_Get, parameter);




            if (prapprovalVM.PRApproval == null) 
            { return NotFound(); }
            return View(prapprovalVM);
         //   return View();
        }
      
        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PRApprovalVM prapprovalVM) 
        { 
            if (ModelState.IsValid)
            {
                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@PRApprovalId", prapprovalVM.PRApproval.PRApprovalId);
                parameter.Add("@PRApprovalTitle ", prapprovalVM.PRApproval.PRApprovalTitle);
                parameter.Add("@PRApprovalDescription ", prapprovalVM.PRApproval.PRApprovalDescription);
                parameter.Add("@WorkOrder ", prapprovalVM.PRApproval.WorkOrder);
                parameter.Add("@CMOA", prapprovalVM.PRApproval.CMOA);
                parameter.Add("@MatCorVer ", prapprovalVM.PRApproval.MatCorVer);
                parameter.Add("@Warranty ", prapprovalVM.PRApproval.Warranty);
                parameter.Add("@WorkDurationSiteDays ", prapprovalVM.PRApproval.WorkDurationSiteDays);
                parameter.Add("@RateSheet ", prapprovalVM.PRApproval.RateSheet);
                parameter.Add("@GateAccess ", prapprovalVM.PRApproval.GateAccess);
                parameter.Add("@RentalPeriodDays ", prapprovalVM.PRApproval.RentalPeriodDays);
                parameter.Add("@EquipmentTireEngine ", prapprovalVM.PRApproval.EquipmentTireEngine);
                parameter.Add("@JustificationVendor ", prapprovalVM.PRApproval.JustificationVendor);
                parameter.Add("@VendorId", prapprovalVM.PRApproval.VendorId);
                parameter.Add("@PurchaseTypeId", prapprovalVM.PRApproval.PurchaseTypeId);
                parameter.Add("@SourcedBy", prapprovalVM.PRApproval.SourcedBy);
                parameter.Add("@ProjectId", prapprovalVM.PRApproval.ProjectId);
                

                if (prapprovalVM.PRApproval.Id==0) // create case whenever no ID posted
                {
                    _unitOfWork.PRApproval.Add(prapprovalVM.PRApproval); // to allow sql procedrues
                   // _unitOfWork.SP_Call.Execute(SD.Proc_PRApproval_Create, parameter);


                }
                else
                {
                    parameter.Add("@Id", prapprovalVM.PRApproval.Id);
                    // _unitOfWork.SP_Call.Execute(SD.Proc_PRApproval_Update, parameter);
                    _unitOfWork.PRApproval.Update(prapprovalVM.PRApproval);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(prapprovalVM);
        
        
        }
       


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

           var allObj = _unitOfWork.PRApproval.GetAll(includeProperties: "Employee,Vendor,PurchaseType,Project"); // commented to allow the link to procedures

           // var allObj = _unitOfWork.SP_Call.List<PRApproval>(SD.Proc_PRApproval_GetAll, null); // to allow stored procedure
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.PRApproval.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

             var objFromDb = _unitOfWork.PRApproval.Get(id);

           // var objFromDb = _unitOfWork.SP_Call.OneRecord<PRApproval>(SD.Proc_PRApproval_Get, parameter);

            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
             _unitOfWork.PRApproval.Remove(objFromDb);

           // _unitOfWork.SP_Call.Execute(SD.Proc_PRApproval_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
