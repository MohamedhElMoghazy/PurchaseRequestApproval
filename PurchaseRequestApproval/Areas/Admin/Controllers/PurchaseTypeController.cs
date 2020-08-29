using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;

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




        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.PurchaseType.GetAll();
            return Json(new { data = allObj });


        }

        #endregion


    }
}
