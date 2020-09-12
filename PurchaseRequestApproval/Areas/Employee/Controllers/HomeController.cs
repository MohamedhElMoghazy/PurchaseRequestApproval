using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
// using PurchaseRequestApproval.Models;
using PurchaseRequestApproval.Models.ViewModels;

namespace PurchaseRequestApproval.Areas.Empolyee.Controllers
{
    [Area("Employee")] // explicity declear the controller is related to the employee area
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<PRApproval> praApprovalList = _unitOfWork.PRApproval.GetAll(includeProperties: "Employee,Vendor,PurchaseType,Project"); // To load the main PRA Approval in the main screen
            return View(praApprovalList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
