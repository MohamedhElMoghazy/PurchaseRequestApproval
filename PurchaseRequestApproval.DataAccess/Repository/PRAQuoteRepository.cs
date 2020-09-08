using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class PRAQuoteRepository : Repository<PRAQuote>, IPRAQuoteRepository
    {
        private readonly ApplicationDbContext _db;

        public PRAQuoteRepository(ApplicationDbContext db): base (db)
        {
            _db = db;
        }

        public void Update(PRAQuote praquote)
        {
            var objFromDb = _db.PRAQuotes.FirstOrDefault(s=>s.Id== praquote.Id);
            if (objFromDb != null)
            {
                objFromDb.PRAQuoteDate = praquote.PRAQuoteDate; // update praquote PRAQuoteDate 
                objFromDb.PRARevision = praquote.PRARevision; // update praquote PRARevision 
                objFromDb.JustificationRev = praquote.JustificationRev; // update praquote JustificationRev 
                objFromDb.EstimatedPrice = praquote.EstimatedPrice; // update praquote EstimatedPrice 
                objFromDb.AddWarCost = praquote.AddWarCost; // update praquote AddWarCost 
                objFromDb.FreightCost = praquote.FreightCost; // update praquote FreightCost 
                objFromDb.EnvFees = praquote.EnvFees; // update praquote EnvFees 
                objFromDb.CarbonTax = praquote.CarbonTax; // update praquote CarbonTax 
                objFromDb.PSTCost = praquote.PSTCost; // update praquote PSTCost 
                objFromDb.Mobilization = praquote.Mobilization; // update praquote 
                objFromDb.SiteOrientation = praquote.SiteOrientation; // update praquote SiteOrientation 
                objFromDb.RentalInsurance = praquote.RentalInsurance; // update praquote RentalInsurance 
                objFromDb.EquipmentDisinfection = praquote.EquipmentDisinfection; // update praquote EquipmentDisinfection 
                objFromDb.ContingencyPercentage = praquote.ContingencyPercentage; // update praquote ContingencyPercentage 
                objFromDb.ContingencyAmount = praquote.ContingencyAmount; // update praquote ContingencyAmount 
                objFromDb.Total = praquote.Total; // update praquote Total 
                objFromDb.PRAId = praquote.PRAId; // update praquote PRAId 
                objFromDb.SubmittedBy = praquote.SubmittedBy; // update praquote SubmittedBy 
                objFromDb.QuoteId = praquote.QuoteId; // update praquote QuoteId 
                objFromDb.QuoteAl1Id = praquote.QuoteAl1Id; // update praquote QuoteAl1Id 
                objFromDb.QuoteAl2Id = praquote.QuoteAl2Id; // update praquote QuoteAl2Id 
                
                





                //_db.SaveChanges();
            }
        }
    }
}
