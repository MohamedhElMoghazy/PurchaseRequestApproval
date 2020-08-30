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
            shipping = _unitOfWork.Shipping.Get(id.GetValueOrDefault());
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
                if (shipping.Id==0) // create case whenever no ID posted
                {
                    _unitOfWork.Shipping.Add(shipping);
                    

                }
                else
                {
                    _unitOfWork.Shipping.Update(shipping);
                    
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

            var allObj = _unitOfWork.Shipping.GetAll();
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.Shipping.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Shipping.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            _unitOfWork.Shipping.Remove(objFromDb);
            _unitOfWork.Save(); 
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
