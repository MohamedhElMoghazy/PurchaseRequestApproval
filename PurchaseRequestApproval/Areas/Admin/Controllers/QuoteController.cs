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
using System.IO;

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

  //      To define a post action method
       [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(QuoteVM quoteVM)
        {
            if (ModelState.IsValid)
            {
                // arrange for files oparations 
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"files\quotes");
                    var extenstion = Path.GetExtension(files[0].FileName);
                   //quoteVM.Quote.PdfUrl = @"\files\quotes\" + fileName + extenstion; // testing for delete file


                    if (quoteVM.Quote.PdfUrl != null)
                    {
                        // this is an edit and we need to remove old image
                        

                        var quotePath = Path.Combine(webRootPath, quoteVM.Quote.PdfUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(quotePath))
                        {
                            System.IO.File.Delete(quotePath);

                        }
                   
                    }
                    using (var filesStreams= new FileStream(Path.Combine(uploads,fileName+extenstion),FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);

                    }
                    quoteVM.Quote.PdfUrl = @"\files\quotes\" + fileName + extenstion;

                }
                else 
                {
                    // update files reference is not changed
                    if (quoteVM.Quote.Id!=0)
                    {
                        Quote objFromDB = _unitOfWork.Quote.Get(quoteVM.Quote.Id);
                        quoteVM.Quote.PdfUrl = objFromDB.PdfUrl;
                    
                    }
                
                
                
                }




                // to pass parameters to sql procedrues
                var parameter = new DynamicParameters();
                parameter.Add("@QuoteDescription", quoteVM.Quote.QuoteDescription);
                parameter.Add("@QuoteAmount", quoteVM.Quote.QuoteAmount);
                parameter.Add("@SiteCompliant", quoteVM.Quote.SiteCompliant);
                parameter.Add("@Less3K", quoteVM.Quote.Less3K);
                parameter.Add("@SoleProvider", quoteVM.Quote.SoleProvider);
                parameter.Add("@OEM", quoteVM.Quote.OEM);
                parameter.Add("@ScheduleDrivenPur", quoteVM.Quote.ScheduleDrivenPur);
                parameter.Add("@Commonality", quoteVM.Quote.Commonality);
                parameter.Add("@FirstNation", quoteVM.Quote.FirstNation);
                parameter.Add("@Metis", quoteVM.Quote.Metis);
                parameter.Add("@LocalVendor", quoteVM.Quote.LocalVendor);
                parameter.Add("@ETADays", quoteVM.Quote.ETADays);
                parameter.Add("@QuoteDate", quoteVM.Quote.QuoteDate);
                parameter.Add("@PdfUrl", quoteVM.Quote.PdfUrl);
                parameter.Add("@VendorId", quoteVM.Quote.VendorId);
                parameter.Add("@VendorContactId", quoteVM.Quote.VendorContactId);
                parameter.Add("@ShippingId", quoteVM.Quote.ShippingId);


                if (quoteVM.Quote.Id == 0) // create case whenever no ID posted
                {
                     _unitOfWork.Quote.Add(quoteVM.Quote); // to allow sql procedrues
                    //_unitOfWork.SP_Call.Execute(SD.Proc_Quote_Create, parameter);


                }
                else
                {
                    parameter.Add("@Id", quoteVM.Quote.Id);
                    //_unitOfWork.SP_Call.Execute(SD.Proc_Quote_Update, parameter);
                    _unitOfWork.Quote.Update(quoteVM.Quote); // to allow sql procedrues

                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            else
            {
                quoteVM.VendorList = _unitOfWork.Vendor.GetAll().Select(i => new SelectListItem
                {
                    Text = i.VendorName,
                    Value = i.Id.ToString()

                });

                quoteVM.VendorContactList = _unitOfWork.VendorContact.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                });
                quoteVM.ShippingList = _unitOfWork.Shipping.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ShippingName,
                    Value = i.Id.ToString()

                });
                if(quoteVM.Quote.Id!=0)
                {
                    quoteVM.Quote = _unitOfWork.Quote.Get(quoteVM.Quote.Id);


                }


            }
            return View(quoteVM.Quote);


        }



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

            string webRootPath = _hostEnvironment.WebRootPath;

            var quotePath = Path.Combine(webRootPath, objFromDb.PdfUrl.TrimStart('\\'));
            if (System.IO.File.Exists(quotePath))
            {
                System.IO.File.Delete(quotePath);

            }




            _unitOfWork.Quote.Remove(objFromDb);

        //    _unitOfWork.SP_Call.Execute(SD.Proc_Quote_Delete, parameter); // Pass parameters to execute sql procedures

            _unitOfWork.Save();
            return Json(new { success = true, Message = "Delete Successful" });

        }

        #endregion


    }
}
