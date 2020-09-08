using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class QuoteRepository : Repository<Quote>, IQuoteRepository
    {
        private readonly ApplicationDbContext _db;

        public QuoteRepository(ApplicationDbContext db): base (db)
        {
            _db = db;
        }

        public void Update(Quote quote)
        {
            var objFromDb = _db.Quotes.FirstOrDefault(s=>s.Id== quote.Id);
            if (objFromDb != null)
            {
                objFromDb.QuoteDescription = quote.QuoteDescription; // update quote QuoteDescription 
                objFromDb.QuoteAmount = quote.QuoteAmount; // update quote QuoteAmount 
                objFromDb.SiteCompliant = quote.SiteCompliant; // update quote SiteCompliant 
                objFromDb.Less3K = quote.Less3K; // update quote Less3K 
                objFromDb.SoleProvider = quote.SoleProvider; // update quote SoleProvider 
                objFromDb.OEM = quote.OEM; // update quote OEM 
                objFromDb.ScheduleDrivenPur = quote.ScheduleDrivenPur; // update quote ScheduleDrivenPur 
                objFromDb.Commonality = quote.Commonality; // update quote Commonality 
                objFromDb.FirstNation = quote.FirstNation; // update quote FirstNation 
                objFromDb.Metis = quote.Metis; // update quote Metis 
                objFromDb.LocalVendor = quote.LocalVendor; // update quote LocalVendor 
                objFromDb.ETADays = quote.ETADays; // update quote ETADays 
                objFromDb.QuoteDate = quote.QuoteDate; // update quote QuoteDate 
                objFromDb.PdfUrl = quote.PdfUrl; // update quote PdfUrl 
                objFromDb.VendorId = quote.VendorId; // update quote VendorId 
                objFromDb.VendorContactId = quote.VendorContactId; // update quote VendorContactId 
                objFromDb.ShippingId = quote.ShippingId; // update quote ShippingId 
                





                //_db.SaveChanges();
            }
        }
    }
}
