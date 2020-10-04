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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = SD.Role_Admin_Modify + "," + SD.Role_Admin_View + "," + SD.Role_Employee_Modify + "," + SD.Role_Employee_View)]// Add authorization Level

    public class PRAQuoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment; // because we will load files into the browser

        //public int PRAIDRecord = 0; // To record the public so we could search custom the screeen according to it
        // public int QuoteIDRecord = 0; // To record the public so we could search custom the screeen according to it


        public PRAQuoteController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment; // for laoding pictures 

        }

        public IActionResult Index()
        {
           // if (PIid == null )
            return View();
        }

        //public IActionResult CheckPRARev(int PRIDRecords)
        //{
        //    this.PRAIDRecord=PRIDRecords; // Set the PRA Record to the required PRI
             


        //    return View("Index",this);
             
        //    //Index();// Call

        //    //if ((_unitOfWork.PRAQuote.GetAll(
        //    //    u => u.PRAId == (PRIDRecords),
        //    //    u => u.OrderBy(x => x.PRARevision)).
        //    //    Count() == 0))
        //    //{ Index(()); }
        //    ////return View();
        //    //int testcount = (_unitOfWork.PRAQuote.GetAll(
        //    //    u => u.PRAId == (PRIDRecords),
        //    //    u => u.OrderBy(x => x.PRARevision)).
        //    //    Count());
        //}

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
                EditExcelPRAQuote(_unitOfWork.PRApproval.Get(praquoteVM.PRAQuote.PRAId).ExcelFileUrl, praquoteVM.PRAQuote);

                return RedirectToAction(nameof(Index)); // if any mistake the name is gotted

            }
            return View(praquoteVM);
        
        
        }

        // Adding function for editing the excel file
        public string EditExcelPRAQuote(string excelpath, PRAQuote praquote)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            byte[] fileContents;
            PRApproval prapproval = _unitOfWork.PRApproval.Get(praquote.PRAId);
            //using (ExcelPackage package = new ExcelPackage(new FileInfo(excelpath.TrimStart('\\'))))
            // string excelpathTrimed = excelpath.Replace(@"\\", @"\");
            string excelpathTrimed = Path.Combine(webRootPath, excelpath.TrimStart('\\'));
            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelpathTrimed)))

            {
                // Configure the work sheets
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                ExcelWorksheet worksheetcover = package.Workbook.Worksheets["Cover Page"];
                // Setting the main PR Area
                worksheet.Cells["F5"].Value = prapproval.PRApprovalId; // Setting the ID
                                                                       // Setup Area Code and Request Type
                worksheet.Cells["F6"].Value = _unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode; // Setting the Area Code
                worksheet.Cells["F7"].Value = _unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurchaseTypeAppr; // Setting the Area Code

                worksheet.Cells["C8"].Value = prapproval.PRApprovalTitle.ToString();
                worksheet.Cells["C10"].Value = prapproval.PRApprovalDescription;

                // Check the data type and fill the code
                if (_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 17)
                {
                    worksheet.Cells["D11"].Value = prapproval.RentalPeriodDays;
                    worksheet.Cells["D12"].Value = prapproval.EquipmentTireEngine;
                    worksheet.Cells["F11"].Value = prapproval.GateAccess;

                }
                else if ((_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 18))
                {
                    worksheet.Cells["D11"].Value = prapproval.CMOA == true ? "Yes" : "No";
                    worksheet.Cells["D12"].Value = prapproval.MatCorVer == true ? "Yes" : "No";
                    worksheet.Cells["F11"].Value = prapproval.Warranty == true ? "Yes" : "No";

                }
                else if ((_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 19))
                {
                    worksheet.Cells["D11"].Value = prapproval.WorkDurationSiteDays;
                    worksheet.Cells["D12"].Value = prapproval.GateAccess == true ? "Yes" : "No";
                    worksheet.Cells["F11"].Value = prapproval.RateSheet == true ? "Yes" : "No";
                }
                else
                {
                    worksheet.Cells["D11"].Value = null;
                    worksheet.Cells["D12"].Value = null;
                    worksheet.Cells["F11"].Value = null;

                }

                worksheet.Cells["F12"].Value = prapproval.WorkOrder; // Set the work order
                worksheet.Cells["C30"].Value = prapproval.JustificationVendor; // Set the Justificaiton for the vendor
                worksheet.Cells["D31"].Value = _unitOfWork.Vendor.Get(prapproval.VendorId).VendorName; // Set the Justificaiton for the vendor
                worksheet.Cells["D60"].Value = _unitOfWork.Employee.Get(prapproval.SourcedBy).EmployeeName; // Set the Submitted by in the Database

                // Arrrange PR Quote Area
                worksheet.Cells["D6"].Value = praquote.PRAQuoteDate.Date;
                worksheet.Cells["D7"].Value = praquote.PRARevision;
                worksheet.Cells["C14"].Value = praquote.JustificationRev;
                worksheet.Cells["D19"].Value = praquote.EstimatedPrice;

                // Check the data type and fill the code

                switch (praquote.PRARevision)
                {
                    case 0:
                        if (_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 17)
                        {
                            worksheet.Cells["D20"].Value = praquote.RentalInsurance;
                            worksheet.Cells["D21"].Value = praquote.Mobilization;
                            worksheet.Cells["D22"].Value = praquote.EquipmentDisinfection;

                        }
                        else if ((_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 18))
                        {
                            worksheet.Cells["D20"].Value = praquote.AddWarCost;
                            worksheet.Cells["D21"].Value = praquote.FreightCost;
                            worksheet.Cells["D22"].Value = praquote.EnvFees;

                        }
                        else if ((_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 19))
                        {
                            worksheet.Cells["D20"].Value = praquote.PSTCost;
                            worksheet.Cells["D21"].Value = praquote.Mobilization;
                            worksheet.Cells["D22"].Value = praquote.SiteOrientation;
                        }
                        else
                        {
                            worksheet.Cells["D20"].Value = null;
                            worksheet.Cells["D21"].Value = null;
                            worksheet.Cells["D22"].Value = null;

                        }
                        break;
                    case 1:
                        if (_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 17)
                        {
                            worksheet.Cells["D20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a=>a.PRAId==praquote.PRAId&& a.PRARevision==0).RentalInsurance;
                            worksheet.Cells["D21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).Mobilization;
                            worksheet.Cells["D22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).EquipmentDisinfection;

                            worksheet.Cells["E20"].Value = praquote.RentalInsurance;
                            worksheet.Cells["E21"].Value = praquote.Mobilization;
                            worksheet.Cells["E22"].Value = praquote.EquipmentDisinfection;


                        }
                        else if ((_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 18))
                        {
                            worksheet.Cells["D20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).AddWarCost;
                            worksheet.Cells["D21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).FreightCost;
                            worksheet.Cells["D22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).EnvFees;


                            worksheet.Cells["E20"].Value = praquote.AddWarCost;
                            worksheet.Cells["E21"].Value = praquote.FreightCost;
                            worksheet.Cells["E22"].Value = praquote.EnvFees;

                        }
                        else if ((_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 19))
                        {
                            worksheet.Cells["D20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).PSTCost;
                            worksheet.Cells["D21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).Mobilization;
                            worksheet.Cells["D22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).SiteOrientation;


                            worksheet.Cells["E20"].Value = praquote.PSTCost;
                            worksheet.Cells["E21"].Value = praquote.Mobilization;
                            worksheet.Cells["E22"].Value = praquote.SiteOrientation;
                        }
                        else
                        {
                            worksheet.Cells["D20"].Value = null;
                            worksheet.Cells["D21"].Value = null;
                            worksheet.Cells["D22"].Value = null;

                            worksheet.Cells["E20"].Value = null;
                            worksheet.Cells["E21"].Value = null;
                            worksheet.Cells["E22"].Value = null;

                        }
                        break;
                    case 2:
                        if (_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 17)
                        {
                            worksheet.Cells["D20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).RentalInsurance;
                            worksheet.Cells["D21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).Mobilization;
                            worksheet.Cells["D22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).EquipmentDisinfection;

                            worksheet.Cells["E20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).RentalInsurance; 
                            worksheet.Cells["E21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).Mobilization;
                            worksheet.Cells["E22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).EquipmentDisinfection;

                            worksheet.Cells["F20"].Value = praquote.RentalInsurance;
                            worksheet.Cells["F21"].Value = praquote.Mobilization;
                            worksheet.Cells["F22"].Value = praquote.EquipmentDisinfection;



                        }
                        else if ((_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 18))
                        {
                            worksheet.Cells["D20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).AddWarCost;
                            worksheet.Cells["D21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).FreightCost;
                            worksheet.Cells["D22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).EnvFees;


                            worksheet.Cells["E20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).AddWarCost;
                            worksheet.Cells["E21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).FreightCost;
                            worksheet.Cells["E22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).EnvFees;

                            worksheet.Cells["F20"].Value = praquote.AddWarCost;
                            worksheet.Cells["F21"].Value = praquote.FreightCost;
                            worksheet.Cells["F22"].Value = praquote.EnvFees;



                        }
                        else if ((_unitOfWork.PurchaseType.Get((prapproval.PurchaseTypeId)).PurcahseCode == 19))
                        {
                            worksheet.Cells["D20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).PSTCost;
                            worksheet.Cells["D21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).Mobilization;
                            worksheet.Cells["D22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 0).SiteOrientation;


                            worksheet.Cells["E20"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).PSTCost;
                            worksheet.Cells["E21"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).Mobilization;
                            worksheet.Cells["E22"].Value = _unitOfWork.PRAQuote.GetFirstOrDefault(a => a.PRAId == praquote.PRAId && a.PRARevision == 1).SiteOrientation;

                            worksheet.Cells["F20"].Value = praquote.PSTCost;
                            worksheet.Cells["F21"].Value = praquote.Mobilization;
                            worksheet.Cells["F22"].Value = praquote.SiteOrientation;
                        }
                        else
                        {
                            worksheet.Cells["D20"].Value = null;
                            worksheet.Cells["D21"].Value = null;
                            worksheet.Cells["D22"].Value = null;

                            worksheet.Cells["E20"].Value = null;
                            worksheet.Cells["E21"].Value = null;
                            worksheet.Cells["E22"].Value = null;

                            worksheet.Cells["F20"].Value = null;
                            worksheet.Cells["F21"].Value = null;
                            worksheet.Cells["F22"].Value = null;

                        }
                        break;
                    default:

                        break;

                }

                worksheet.Cells["D23"].Value = praquote.CarbonTax;
                //worksheet.Cells["D24"].Value = praquote.CarbonTax;
                worksheet.Cells["D25"].Value = praquote.ContingencyPercentage;
                //worksheet.Cells["D26"].Value = praquote.ContingencyAmount;
                // worksheet.Cells["D23"].Value = praquote.Total;
                worksheet.Cells["D32"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).QuoteAmount;
                worksheet.Cells["D33"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).SiteCompliant==true?"Yes": "N/A";
                worksheet.Cells["D34"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).Less3K==true? "Yes":"No";
                worksheet.Cells["D35"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).SoleProvider == true ? "Yes" : "No";
                worksheet.Cells["D36"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).OEM == true ? "Yes" : "No";
                worksheet.Cells["D37"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).ScheduleDrivenPur == true ? "Yes" : "No";
                worksheet.Cells["F33"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).Commonality == true ? "Yes" : "No";
                worksheet.Cells["F34"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).FirstNation == true ? "Yes" : "No";
                worksheet.Cells["F35"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).Metis == true ? "Yes" : "No";
                worksheet.Cells["F36"].Value = _unitOfWork.Quote.Get(praquote.QuoteId).LocalVendor == true ? "Yes" : "No";

                worksheet.Cells["D38"].Value = _unitOfWork.VendorContact.Get(_unitOfWork.Quote.Get(praquote.QuoteId).VendorContactId).Name;
                worksheet.Cells["E38"].Value = _unitOfWork.VendorContact.Get(_unitOfWork.Quote.Get(praquote.QuoteId).VendorContactId).Email;

                // Data for Alternate qutoe1
                if(praquote.QuoteAl1Id!=null)
                {
                    worksheet.Cells["D43"].Value = _unitOfWork.Vendor.Get((_unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).VendorId)).VendorName;
                    worksheet.Cells["D44"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).QuoteAmount;
                    worksheet.Cells["D45"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).SiteCompliant == true ? "Yes" : "N/A";
                    worksheet.Cells["D46"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).Less3K == true ? "Yes" : "No";
                    worksheet.Cells["D47"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).SoleProvider == true ? "Yes" : "No";
                    worksheet.Cells["D48"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).OEM == true ? "Yes" : "No";
                    worksheet.Cells["D49"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).ScheduleDrivenPur == true ? "Yes" : "No";
                    worksheet.Cells["F45"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).Commonality == true ? "Yes" : "No";
                    worksheet.Cells["F46"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).FirstNation == true ? "Yes" : "No";
                    worksheet.Cells["F47"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).Metis == true ? "Yes" : "No";
                    worksheet.Cells["F48"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl1Id).LocalVendor == true ? "Yes" : "No";

                }
                else
                {
                    worksheet.Cells["D43"].Value = null;
                    worksheet.Cells["D44"].Value = null;
                    worksheet.Cells["D45"].Value = null;
                    worksheet.Cells["D46"].Value = null; 
                    worksheet.Cells["D47"].Value = null;
                    worksheet.Cells["D48"].Value = null; 
                    worksheet.Cells["D49"].Value = null;
                    worksheet.Cells["F45"].Value = null; 
                    worksheet.Cells["F46"].Value = null; 
                    worksheet.Cells["F47"].Value = null; 
                    worksheet.Cells["F48"].Value = null;
                }

                // Data for Alternate qutoe2
                if (praquote.QuoteAl2Id != null)
                {
                    worksheet.Cells["D51"].Value = _unitOfWork.Vendor.Get((_unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).VendorId)).VendorName;
                    worksheet.Cells["D52"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).QuoteAmount;
                    worksheet.Cells["D53"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).SiteCompliant == true ? "Yes" : "N/A";
                    worksheet.Cells["D54"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).Less3K == true ? "Yes" : "No";
                    worksheet.Cells["D55"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).SoleProvider == true ? "Yes" : "No";
                    worksheet.Cells["D56"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).OEM == true ? "Yes" : "No";
                    worksheet.Cells["D57"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).ScheduleDrivenPur == true ? "Yes" : "No";
                    worksheet.Cells["F53"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).Commonality == true ? "Yes" : "No";
                    worksheet.Cells["F54"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).FirstNation == true ? "Yes" : "No";
                    worksheet.Cells["F55"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).Metis == true ? "Yes" : "No";
                    worksheet.Cells["F56"].Value = _unitOfWork.Quote.Get((int)praquote.QuoteAl2Id).LocalVendor == true ? "Yes" : "No";

                }
                else
                {
                    worksheet.Cells["D51"].Value = null;
                    worksheet.Cells["D52"].Value = null;
                    worksheet.Cells["D53"].Value = null;
                    worksheet.Cells["D54"].Value = null;
                    worksheet.Cells["D55"].Value = null;
                    worksheet.Cells["D56"].Value = null;
                    worksheet.Cells["D57"].Value = null;
                    worksheet.Cells["F53"].Value = null;
                    worksheet.Cells["F54"].Value = null;
                    worksheet.Cells["F55"].Value = null;
                    worksheet.Cells["F56"].Value = null;
                }

                worksheet.Cells["D59"].Value = _unitOfWork.Employee.Get(praquote.SubmittedBy).EmployeeName; // Set the Submitted by in the Database





































                // Arrange for cover page
                worksheetcover.Cells["B6"].Value = "Contract No. " + _unitOfWork.Project.Get(prapproval.ProjectId).ContractNo.ToString();
                worksheetcover.Cells["B7"].Value = "WPI " + _unitOfWork.Project.Get(prapproval.ProjectId).WorkPackageIn.ToString() + ": Site Services & Maintenance";


                package.Save();

                fileContents = package.GetAsByteArray();
                //package.SaveAs(FileInfo(excelpathTrimed));

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                //Response.BinaryWrite(package.GetAsByteArray());  // send the file
                // Response.End();
                // Response.BinaryWrite(package.GetAsByteArray());
                //Response.End();





                // clear the work area
                // Response.Clear();
                //Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                //Response.BinaryWrite(package.GetAsByteArray());




            }

            if (fileContents == null || fileContents.Length == 0)
            { return null; }


            File(
               fileContents: fileContents,
               contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");



            /*
             return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "StudinetList.xlsx");
            
            */


            return "hi";

        }






        // End of editing the excel file    

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {
            // Quote1 and Quote 2 has been removed from including properties to avoid the errors
            // adding select criteria for the data on the screen
            //if (PRAIDRecord == 0)
            //{
            //    // commented to allow the link to procedures
            //    var allObj = _unitOfWork.PRAQuote.GetAll(includeProperties: "PRApproval,Employee,Quote");
            //    return Json(new { data = allObj });

            //}
            //else
            //{
            //    // Allow to show all properties for certain ID
            //    var allObj = _unitOfWork.PRAQuote.GetAll(
            //    u => u.PRAId == (PRAIDRecord),
            //    u => u.OrderBy(x => x.PRARevision),includeProperties: "PRApproval,Employee,Quote");

            //    return Json(new { data = allObj });

            //}

            // commented to allow the link to procedures
            var allObj = _unitOfWork.PRAQuote.GetAll(includeProperties: "PRApproval,Employee,Quote");
            return Json(new { data = allObj });

            // var allObj = _unitOfWork.SP_Call.List<PRAQuote>(SD.Proc_PRAQuote_GetAll, null); // to allow stored procedure


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
