using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using PurchaseRequestApproval.Models.ViewModels;
using PurchaseRequestApproval.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment; // because we will load files into the browser
      
        
        // public QuoteController(IUnitOfWork unitOfWork) // before loading the host environment 
        public QuoteController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment; // for laoding pictures 
        }

        public IActionResult Index()
        {
            return View();
        }

        // create an method for upsert and can get null Id in case of create 
        public IActionResult Upsert(int? id)
        {

            QuoteVM quoteVM = new QuoteVM()
            {
                Quote = new Quote(),
                VendorList=_unitOfWork.Vendor.GetAll().Select(i=>new SelectListItem { 
                Text= i.VendorName,
                Value=i.Id.ToString()
                
                }),

                VendorContactList = _unitOfWork.VendorContact.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                }),
                ShippingList = _unitOfWork.Shipping.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ShippingName,
                    Value = i.Id.ToString()

                })






            } ;
            if (id ==null) // create case
            {
                return View(quoteVM);

            }
            // this for edit request
             quoteVM.Quote = _unitOfWork.Quote.Get(id.GetValueOrDefault()); // To add stored procedure

            //var parameter = new DynamicParameters(); // arrange parameters for sql server
            //parameter.Add("@Id", id); // arrange to send the Id

            // var objFromDb = _unitOfWork.Quote.Get(id);

            //quote = _unitOfWork.SP_Call.OneRecord<Quote>(SD.Proc_Quote_Get, parameter);




            if (quoteVM.Quote == null) 
            { return NotFound(); }
            return View(quoteVM);
         //   return View();
        }

        // To define a post action method 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(Quote quote) 
        //{ 
        //    if (ModelState.IsValid)
        //    {
        //        // to pass parameters to sql procedrues
        //        var parameter = new DynamicParameters();
        //        parameter.Add("@QuoteDescription", quote.QuoteDescription);
        //        parameter.Add("@QuoteAmount", quote.QuoteAmount);
        //        parameter.Add("@SiteCompliant", quote.SiteCompliant);
        //        parameter.Add("@Less3K", quote.Less3K);
        //        parameter.Add("@SoleProvider", quote.SoleProvider);
        //        parameter.Add("@OEM", quote.OEM);
        //        parameter.Add("@ScheduleDrivenPur", quote.ScheduleDrivenPur);
        //        parameter.Add("@Commonality", quote.Commonality);
        //        parameter.Add("@FirstNation", quote.FirstNation);
        //        parameter.Add("@Metis", quote.Metis);
        //        parameter.Add("@LocalVendor", quote.LocalVendor);
        //        parameter.Add("@ETADays", quote.ETADays);
        //        parameter.Add("@QuoteDate", quote.QuoteDate);
        //        parameter.Add("@PdfUrl", quote.PdfUrl);
        //        parameter.Add("@VendorId", quote.VendorId);
        //        parameter.Add("@VendorContactId", quote.VendorContactId);
        //        parameter.Add("@ShippingId", quote.ShippingId);
                
                
        //        if (quote.Id==0) // create case whenever no ID posted
        //        {
        //            // _unitOfWork.Quote.Add(quote); // to allow sql procedrues
        //            _unitOfWork.SP_Call.Execute(SD.Proc_Quote_Create, parameter);


        //        }
        //        else
        //        {
        //            parameter.Add("@Id", quote.Id);
        //            _unitOfWork.SP_Call.Execute(SD.Proc_Quote_Update, parameter);

        //        }
        //        _unitOfWork.Save();
        //        return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

        //    }
        //    return View(quote);
        
        
        //}



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

             var allObj = _unitOfWork.Quote.GetAll(includeProperties: "Vendor,VendorContact,Shipping"); // commented to allow the link to procedures

            //var allObj = _unitOfWork.SP_Call.List<Quote>(SD.Proc_Quote_GetAll, null); // to allow stored procedure
            return Json(new { data = allObj });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.Quote.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters(); // arrange parameters for sql server
            parameter.Add("@Id", id); // arrange to send the Id

             var objFromDb = _unitOfWork.Quote.Get(id);

         //   var objFromDb = _unitOfWork.SP_Call.OneRecord<Quote>(SD.Proc_Quote_Get, parameter);

            if (objFromDb == null)
            {
                return Json(new { success = false, Message = "Error while deleting" });
            }
             _unitOfWork.Quote.Remove(objFromDb);

        //    _unitOfWork.SP_Call.Execute(SD.Proc_Quote_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
