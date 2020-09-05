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
    public class VendorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VendorController(IUnitOfWork unitOfWork)
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
            Vendor vendor = new Vendor();
            if (id ==null) // create case
            {
                return View(vendor);

            }
            // this for edit request
            // vendor = _unitOfWork.Vendor.Get(id.GetValueOrDefault());

            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

            //var objFromDb = _unitOfWork.Vendor.Get(id);

            vendor = _unitOfWork.SP_Call.OneRecord<Vendor>(SD.Proc_Vendor_Get, parameter);

            if (vendor == null) 
            { return NotFound(); }
            return View(vendor);
         //   return View();
        }

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Vendor vendor) 
        { 
            if (ModelState.IsValid)
            {

                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@VendorName ", vendor.VendorName);
                parameter.Add("@VendoreCode", vendor.VendoreCode);
                parameter.Add("@VendorAddress", vendor.VendorAddress);
                parameter.Add("@VendorPhone", vendor.VendorPhone);
                parameter.Add("@SalesContactName", vendor.SalesContactName);
                parameter.Add("@SalesContactEmail", vendor.SalesContactEmail);
                parameter.Add("@AccountContactName",vendor.AccountContactName);
                parameter.Add("@AccContactEmail", vendor.AccContactEmail);
                parameter.Add("@RegVendor", vendor.RegVendor);

                if (vendor.Id==0) // create case whenever no ID posted
                {
                    //_unitOfWork.Vendor.Add(vendor);
                 _unitOfWork.SP_Call.Execute(SD.Proc_Vendor_Create, parameter);

                }
                else
                {
                    //_unitOfWork.Vendor.Update(vendor);
                    parameter.Add("@Id", vendor.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Vendor_Update, parameter);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(vendor);
        
        
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            // var allObj = _unitOfWork.Vendor.GetAll();

            var allObj = _unitOfWork.SP_Call.List<Vendor>(SD.Proc_Vendor_GetAll, null); // to allow stored procedure
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.Vendor.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

            //var objFromDb = _unitOfWork.Vendor.Get(id);

            var objFromDb = _unitOfWork.SP_Call.OneRecord<Vendor>(SD.Proc_Vendor_Get, parameter);
            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            //_unitOfWork.Vendor.Remove(objFromDb);

            _unitOfWork.SP_Call.Execute(SD.Proc_Vendor_Delete, parameter); // Pass parameters to execute sql procedures
            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
