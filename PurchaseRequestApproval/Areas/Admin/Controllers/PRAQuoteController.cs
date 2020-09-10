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

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        /*

        // To define a post action method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PRAQuote praquote) 
        { 
            if (ModelState.IsValid)
            {
                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@PRAQuoteDate", praquote.PRAQuoteDate);
                parameter.Add("@PRARevision", praquote.PRARevision);
                parameter.Add("@JustificationRev", praquote.JustificationRev);
                parameter.Add("@EstimatedPrice", praquote.EstimatedPrice);
                parameter.Add("@AddWarCost", praquote.AddWarCost);
                parameter.Add("@FreightCost", praquote.FreightCost);
                parameter.Add("@EnvFees", praquote.EnvFees);
                parameter.Add("@CarbonTax", praquote.CarbonTax);
                parameter.Add("@PSTCost", praquote.PSTCost);
                parameter.Add("@Mobilization", praquote.Mobilization);
                parameter.Add("@SiteOrientation", praquote.SiteOrientation);
                parameter.Add("@RentalInsurance", praquote.RentalInsurance);
                parameter.Add("@EquipmentDisinfection", praquote.EquipmentDisinfection);
                parameter.Add("@ContingencyPercentage", praquote.ContingencyPercentage);
                parameter.Add("@ContingencyAmount", praquote.ContingencyAmount);
                parameter.Add("@Total", praquote.Total);
                parameter.Add("@PRAId", praquote.PRAId);
                parameter.Add("@SubmittedBy ", praquote.SubmittedBy);
                parameter.Add("@QuoteId", praquote.QuoteId);
                parameter.Add("@QuoteAl1Id", praquote.QuoteAl1Id);
                parameter.Add("@QuoteAl2Id", praquote.QuoteAl2Id);











                if (praquote.Id==0) // create case whenever no ID posted
                {
                    // _unitOfWork.PRAQuote.Add(praquote); // to allow sql procedrues
                    _unitOfWork.SP_Call.Execute(SD.Proc_PRAQuote_Create, parameter);


                }
                else
                {
                    parameter.Add("@Id", praquote.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_PRAQuote_Update, parameter);

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(praquote);
        
        
        }

            */

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
