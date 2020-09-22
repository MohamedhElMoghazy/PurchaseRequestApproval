using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using PurchaseRequestApproval.Utility;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin_Modify + "," + SD.Role_Admin_View)]// Add authorization Level

    public class PurchaseTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseTypeController(IUnitOfWork unitOfWork)
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
            PurchaseType purchaseType = new PurchaseType();
            if (id ==null) // create case
            {
                return View(purchaseType);

            }
            // this for edit request
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id
            purchaseType = _unitOfWork.SP_Call.OneRecord<PurchaseType>(SD.Proc_PurchaseType_Get, parameter);


            if (purchaseType == null) 
            { return NotFound(); }
            return View(purchaseType);
         //   return View();
        }

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin_Modify)]// Add authorization Level

        public IActionResult Upsert(PurchaseType purchaseType) 
        { 
            if (ModelState.IsValid)
            {

                var parameter = new DynamicParameters();
                parameter.Add("@PurcahseTypeName", purchaseType.PurcahseTypeName);
                parameter.Add("@PurcahseCode", purchaseType.PurcahseCode);
                parameter.Add("@PurchaseTypeAppr", purchaseType.PurchaseTypeAppr);

                if (purchaseType.Id==0) // create case whenever no ID posted
                {
                    _unitOfWork.PurchaseType.Add(purchaseType);
                    //_unitOfWork.SP_Call.Execute(SD.Proc_PurchaseType_Create, parameter);


                }
                else
                {
                    parameter.Add("@Id", purchaseType.Id);
                    //_unitOfWork.SP_Call.Execute(SD.Proc_PurchaseType_Update , parameter);
                    _unitOfWork.PurchaseType.Update(purchaseType);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(purchaseType);
        
        
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            // var allObj = _unitOfWork.PurchaseType.GetAll(); now commented to use the stored procedure
            // var allObj = _unitOfWork.SP_Call.List<PurchaseType>(SD.Proc_PurchaseType_GetAll,null);
            var allObj = _unitOfWork.SP_Call.List<PurchaseType>(SD.Proc_PurchaseType_GetAll, null);
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.PurchaseType.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }
        [HttpDelete]
        [Authorize(Roles = SD.Role_Admin_Modify)]// Add authorization Level

        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id
            var objFromDb = _unitOfWork.SP_Call.OneRecord<PurchaseType>(SD.Proc_PurchaseType_Get,parameter);
            // var objFromDb = _unitOfWork.PurchaseType.Get(id); // to let using the sql server
            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            // _unitOfWork.PurchaseType.Remove(objFromDb); // Cmmented to trasfer to sql procedures
            _unitOfWork.SP_Call.Execute(SD.Proc_PurchaseType_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
