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
            PRApproval prapproval = new PRApproval();
            if (id ==null) // create case
            {
                return View(prapproval);

            }
            // this for edit request
            // prapproval = _unitOfWork.PRApproval.Get(id.GetValueOrDefault()); // To add stored procedure

            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

            // var objFromDb = _unitOfWork.PRApproval.Get(id);

            prapproval = _unitOfWork.SP_Call.OneRecord<PRApproval>(SD.Proc_PRApproval_Get, parameter);




            if (prapproval == null) 
            { return NotFound(); }
            return View(prapproval);
         //   return View();
        }

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PRApproval prapproval) 
        { 
            if (ModelState.IsValid)
            {
                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@PRApprovalId", prapproval.PRApprovalId);
                parameter.Add("@PRApprovalTitle ", prapproval.PRApprovalTitle);
                parameter.Add("@PRApprovalDescription ", prapproval.PRApprovalDescription);
                parameter.Add("@WorkOrder ", prapproval.WorkOrder);
                parameter.Add("@CMOA", prapproval.CMOA);
                parameter.Add("@MatCorVer ", prapproval.MatCorVer);
                parameter.Add("@Warranty ", prapproval.Warranty);
                parameter.Add("@WorkDurationSiteDays ", prapproval.WorkDurationSiteDays);
                parameter.Add("@RateSheet ", prapproval.RateSheet);
                parameter.Add("@GateAccess ", prapproval.GateAccess);
                parameter.Add("@RentalPeriodDays ", prapproval.RentalPeriodDays);
                parameter.Add("@EquipmentTireEngine ", prapproval.EquipmentTireEngine);
                parameter.Add("@JustificationVendor ", prapproval.JustificationVendor);
                parameter.Add("@VendorId", prapproval.VendorId);
                parameter.Add("@PurchaseTypeId", prapproval.PurchaseTypeId);
                parameter.Add("@SourcedBy", prapproval.SourcedBy);
                parameter.Add("@ProjectId", prapproval.ProjectId);
                

                if (prapproval.Id==0) // create case whenever no ID posted
                {
                    // _unitOfWork.PRApproval.Add(prapproval); // to allow sql procedrues
                    _unitOfWork.SP_Call.Execute(SD.Proc_PRApproval_Create, parameter);


                }
                else
                {
                    parameter.Add("@Id", prapproval.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_PRApproval_Update, parameter);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(prapproval);
        
        
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            // var allObj = _unitOfWork.PRApproval.GetAll(); // commented to allow the link to procedures

            var allObj = _unitOfWork.SP_Call.List<PRApproval>(SD.Proc_PRApproval_GetAll, null); // to allow stored procedure
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

            // var objFromDb = _unitOfWork.PRApproval.Get(id);

            var objFromDb = _unitOfWork.SP_Call.OneRecord<PRApproval>(SD.Proc_PRApproval_Get, parameter);

            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            // _unitOfWork.PRApproval.Remove(objFromDb);

            _unitOfWork.SP_Call.Execute(SD.Proc_PRApproval_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
