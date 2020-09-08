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
    public class VendorContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VendorContactController(IUnitOfWork unitOfWork)
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
            VendorContact vendorcontact = new VendorContact();
            if (id ==null) // create case
            {
                return View(vendorcontact);

            }
            // this for edit request
            // vendorcontact = _unitOfWork.VendorContact.Get(id.GetValueOrDefault()); // To add stored procedure

            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

            // var objFromDb = _unitOfWork.VendorContact.Get(id);

            vendorcontact = _unitOfWork.SP_Call.OneRecord<VendorContact>(SD.Proc_VendorContact_Get, parameter);




            if (vendorcontact == null) 
            { return NotFound(); }
            return View(vendorcontact);
         //   return View();
        }

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(VendorContact vendorcontact) 
        { 
            if (ModelState.IsValid)
            {
                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@Name", vendorcontact.Name);
                parameter.Add("@Email", vendorcontact.Email);
                parameter.Add("@Branch", vendorcontact.Branch);
                parameter.Add("@Remark", vendorcontact.Remark);
                parameter.Add("@VendorId", vendorcontact.VendorId);
                


                if (vendorcontact.Id==0) // create case whenever no ID posted
                {
                    // _unitOfWork.VendorContact.Add(vendorcontact); // to allow sql procedrues
                    _unitOfWork.SP_Call.Execute(SD.Proc_VendorContact_Create, parameter);


                }
                else
                {
                    parameter.Add("@Id", vendorcontact.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_VendorContact_Update, parameter);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(vendorcontact);
        
        
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            // var allObj = _unitOfWork.VendorContact.GetAll(); // commented to allow the link to procedures

            var allObj = _unitOfWork.SP_Call.List<VendorContact>(SD.Proc_VendorContact_GetAll, null); // to allow stored procedure
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.VendorContact.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

            // var objFromDb = _unitOfWork.VendorContact.Get(id);

            var objFromDb = _unitOfWork.SP_Call.OneRecord<VendorContact>(SD.Proc_VendorContact_Get, parameter);

            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            // _unitOfWork.VendorContact.Remove(objFromDb);

            _unitOfWork.SP_Call.Execute(SD.Proc_VendorContact_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
