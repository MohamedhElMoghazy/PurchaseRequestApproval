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
            vendor = _unitOfWork.Vendor.Get(id.GetValueOrDefault());
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
                if (vendor.Id==0) // create case whenever no ID posted
                {
                    _unitOfWork.Vendor.Add(vendor);
                    

                }
                else
                {
                    _unitOfWork.Vendor.Update(vendor);
                    
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

            var allObj = _unitOfWork.Vendor.GetAll();
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
            var objFromDb = _unitOfWork.Vendor.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
            _unitOfWork.Vendor.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
