using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            purchaseType = _unitOfWork.PurchaseType.Get(id.GetValueOrDefault());
            if (purchaseType == null) 
            { return NotFound(); }
            return View(purchaseType);
         //   return View();
        }





        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            var allObj = _unitOfWork.PurchaseType.GetAll();
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.PurchaseType.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }

        #endregion


    }
}
