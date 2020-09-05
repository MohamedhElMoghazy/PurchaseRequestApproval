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
    public class ShippingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShippingController(IUnitOfWork unitOfWork)
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
            Shipping shipping = new Shipping();
            if (id ==null) // create case
            {
                return View(shipping);

            }
            // this for edit request
            //shipping = _unitOfWork.Shipping.Get(id.GetValueOrDefault());
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id


            // var objFromDb = _unitOfWork.Shipping.Get(id);

            shipping= _unitOfWork.SP_Call.OneRecord<Shipping>(SD.Proc_Shipping_Get, parameter);

            if (shipping == null) 
            { return NotFound(); }
            return View(shipping);
         //   return View();
        }

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Shipping shipping) 
        { 
            if (ModelState.IsValid)
            {
                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@ShippingName", shipping.ShippingName);
                parameter.Add("@ShippingDescription", shipping.ShippingDescription);
                parameter.Add("@ShippingACC", shipping.ShippingACC);
                

                if (shipping.Id==0) // create case whenever no ID posted
                {
                    //_unitOfWork.Shipping.Add(shipping);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Shipping_Create, parameter);


                }
                else
                {
                    //_unitOfWork.Shipping.Update(shipping);
                    parameter.Add("@Id", shipping.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Shipping_Update, parameter);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(shipping);
        
        
        }



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            // var allObj = _unitOfWork.Shipping.GetAll();
            var allObj = _unitOfWork.SP_Call.List<Shipping>(SD.Proc_Shipping_GetAll, null); // to allow stored procedure
            return Json(new { data = allObj });


            


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id


            // var objFromDb = _unitOfWork.Shipping.Get(id);

            var objFromDb = _unitOfWork.SP_Call.OneRecord<Shipping>(SD.Proc_Shipping_Get, parameter);

            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            //_unitOfWork.Shipping.Remove(objFromDb);
            _unitOfWork.SP_Call.Execute(SD.Proc_Shipping_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save(); 
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
