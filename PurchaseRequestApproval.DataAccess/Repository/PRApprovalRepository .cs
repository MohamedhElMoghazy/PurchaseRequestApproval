using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class PRApprovalRepository : Repository<PRApproval>, IPRApprovalRepository
    {
        private readonly ApplicationDbContext _db;

        public PRApprovalRepository(ApplicationDbContext db): base (db)
        {
            _db = db;
        }

        public void Update(PRApproval prapproval)
        {
            var objFromDb = _db.PRApprovals.FirstOrDefault(s=>s.Id== prapproval.Id);
            if (objFromDb != null)
            {
                objFromDb.PRApprovalTitle = prapproval.PRApprovalTitle; // update prapproval PRApprovalTitle 
                objFromDb.PRApprovalDescription = prapproval.PRApprovalDescription; // update prapproval PRApprovalDescription 
                objFromDb.WorkOrder = prapproval.WorkOrder; // update prapproval WorkOrder 
                objFromDb.CMOA = prapproval.CMOA; // update prapproval CMOA 
                objFromDb.MatCorVer = prapproval.MatCorVer; // update prapproval MatCorVer 
                objFromDb.Warranty = prapproval.Warranty; // update prapproval Warranty 
                objFromDb.WorkDurationSiteDays = prapproval.WorkDurationSiteDays; // update prapproval WorkDurationSiteDays 
                objFromDb.RateSheet = prapproval.RateSheet; // update prapproval RateSheet 
                objFromDb.GateAccess = prapproval.GateAccess; // update prapproval GateAccess 
                objFromDb.RentalPeriodDays = prapproval.RentalPeriodDays; // update prapproval RentalPeriodDays 
                objFromDb.EquipmentTireEngine = prapproval.EquipmentTireEngine; // update prapproval EquipmentTireEngine 
                objFromDb.JustificationVendor = prapproval.JustificationVendor; // update prapproval JustificationVendor 
                objFromDb.VendorId = prapproval.VendorId; // update prapproval VendorId 
                objFromDb.PurchaseTypeId = prapproval.PurchaseTypeId; // update prapproval PurchaseTypeId 
                objFromDb.SourcedBy = prapproval.SourcedBy; // update prapproval SourcedBy 
                objFromDb.ProjectId = prapproval.ProjectId; // update prapproval site





                //_db.SaveChanges();
            }
        }
    }
}
