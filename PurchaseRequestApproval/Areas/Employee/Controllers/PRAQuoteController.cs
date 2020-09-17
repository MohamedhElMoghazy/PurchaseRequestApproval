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
using Microsoft.AspNetCore.Mvc.Rendering;
using PurchaseRequestApproval.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = SD.Role_Admin_Modify + "," + SD.Role_Admin_View + "," + SD.Role_Employee_Modify + "," + SD.Role_Employee_View)]// Add authorization Level

    public class PRAQuoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PRAQuoteController(IUnitOfWork unitOfWork)
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
            PRAQuoteVM praquoteVM = new PRAQuoteVM() 
            {
                PRAQuote = new PRAQuote(),
                PRApprovalList = _unitOfWork.PRApproval.GetAll().Select(i => new SelectListItem
                {
                    Text = i.PRApprovalId.ToString(),
                    Value = i.Id.ToString()

                }),

                EmployeeList = _unitOfWork.Employee.GetAll().Select(i => new SelectListItem
                {
                    Text = i.EmployeeName,
                    Value = i.Id.ToString()

                }),

                QuoteList = _unitOfWork.Quote.GetAll().Select(i => new SelectListItem
                {
                    Text = i.QuoteDescription,
                    Value = i.Id.ToString()

                }),

                QuoteList1 = _unitOfWork.Quote.GetAll().Select(i => new SelectListItem
                {
                    Text = i.QuoteDescription,
                    Value = i.Id.ToString()

                }),

                QuoteList2 = _unitOfWork.Quote.GetAll().Select(i => new SelectListItem
                {
                    Text = i.QuoteDescription,
                    Value = i.Id.ToString()

                })










            };
            if (id ==null) // create case
            {
                return View(praquoteVM);

            }
            // this for edit request
             praquoteVM.PRAQuote = _unitOfWork.PRAQuote.Get(id.GetValueOrDefault()); // To add stored procedure

           // var parameter = new DynamicParameters(); // arrange parameters for sql server
            //parameter.Add("@Id", id); // arrange to send the Id

            // var objFromDb = _unitOfWork.PRAQuote.Get(id);

            //praquote = _unitOfWork.SP_Call.OneRecord<PRAQuote>(SD.Proc_PRAQuote_Get, parameter);




            if (praquoteVM.PRAQuote == null) 
            { return NotFound(); }
            return View(praquoteVM);
         //   return View();
        }

        

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin_Modify + "," + SD.Role_Admin_View + "," + SD.Role_Employee_Modify)]// Add authorization Level

        public IActionResult Upsert(PRAQuoteVM praquoteVM) 
        { 
            if (ModelState.IsValid)
            {
                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@PRAQuoteDate", praquoteVM.PRAQuote.PRAQuoteDate);
                parameter.Add("@PRARevision", praquoteVM.PRAQuote.PRARevision);
                parameter.Add("@JustificationRev", praquoteVM.PRAQuote.JustificationRev);
                parameter.Add("@EstimatedPrice", praquoteVM.PRAQuote.EstimatedPrice);
                parameter.Add("@AddWarCost", praquoteVM.PRAQuote.AddWarCost);
                parameter.Add("@FreightCost", praquoteVM.PRAQuote.FreightCost);
                parameter.Add("@EnvFees", praquoteVM.PRAQuote.EnvFees);
                parameter.Add("@CarbonTax", praquoteVM.PRAQuote.CarbonTax);
                parameter.Add("@PSTCost", praquoteVM.PRAQuote.PSTCost);
                parameter.Add("@Mobilization", praquoteVM.PRAQuote.Mobilization);
                parameter.Add("@SiteOrientation", praquoteVM.PRAQuote.SiteOrientation);
                parameter.Add("@RentalInsurance", praquoteVM.PRAQuote.RentalInsurance);
                parameter.Add("@EquipmentDisinfection", praquoteVM.PRAQuote.EquipmentDisinfection);
                parameter.Add("@ContingencyPercentage", praquoteVM.PRAQuote.ContingencyPercentage);
                parameter.Add("@ContingencyAmount", praquoteVM.PRAQuote.ContingencyAmount);
                parameter.Add("@Total", praquoteVM.PRAQuote.Total);
                parameter.Add("@PRAId", praquoteVM.PRAQuote.PRAId);
                parameter.Add("@SubmittedBy ", praquoteVM.PRAQuote.SubmittedBy);
                parameter.Add("@QuoteId", praquoteVM.PRAQuote.QuoteId);
                parameter.Add("@QuoteAl1Id", praquoteVM.PRAQuote.QuoteAl1Id);
                parameter.Add("@QuoteAl2Id", praquoteVM.PRAQuote.QuoteAl2Id);











                if (praquoteVM.PRAQuote.Id==0) // create case whenever no ID posted
                {
                     _unitOfWork.PRAQuote.Add(praquoteVM.PRAQuote); // to allow sql procedrues
                    //_unitOfWork.SP_Call.Execute(SD.Proc_PRAQuote_Create, parameter);


                }
                else
                {
                    parameter.Add("@Id", praquoteVM.PRAQuote.Id);
                    // _unitOfWork.SP_Call.Execute(SD.Proc_PRAQuote_Update, parameter);
                    _unitOfWork.PRAQuote.Update(praquoteVM.PRAQuote); // to allow sql procedrues

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(praquoteVM);
        
        
        }

            

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {
            // Quote1 and Quote 2 has been removed from including properties to avoid the errors
            var allObj = _unitOfWork.PRAQuote.GetAll(includeProperties: "PRApproval,Employee,Quote"); // commented to allow the link to procedures

           // var allObj = _unitOfWork.SP_Call.List<PRAQuote>(SD.Proc_PRAQuote_GetAll, null); // to allow stored procedure
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.PRAQuote.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }
        [HttpDelete]
        [Authorize(Roles = SD.Role_Admin_Modify + "," + SD.Role_Admin_View + "," + SD.Role_Employee_Modify)]// Add authorization Level

        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

             var objFromDb = _unitOfWork.PRAQuote.Get(id);

           // var objFromDb = _unitOfWork.SP_Call.OneRecord<PRAQuote>(SD.Proc_PRAQuote_Get, parameter);

            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
             _unitOfWork.PRAQuote.Remove(objFromDb);

            //_unitOfWork.SP_Call.Execute(SD.Proc_PRAQuote_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
