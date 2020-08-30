using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        private readonly ApplicationDbContext _db;

        public VendorRepository(ApplicationDbContext db): base (db)
        {
            _db = db;
        }

        public void Update(Vendor vendor)
        {
            var objFromDb = _db.Vendors.FirstOrDefault(s=>s.Id== vendor.Id);
            if (objFromDb != null)
            {
                objFromDb.VendorName = vendor.VendorName; // update vendor name
                objFromDb.VendoreCode = vendor.VendoreCode; // update vendor code
                objFromDb.VendorAddress = vendor.VendorAddress; // update vendor Address
                objFromDb.VendorPhone = vendor.VendorPhone; // update vendor phone
                objFromDb.SalesContactName = vendor.SalesContactName; // update vendor name
                objFromDb.SalesContactEmail = vendor.SalesContactEmail; // update vendor name
                objFromDb.AccountContactName = vendor.AccountContactName; // update vendor name
                objFromDb.AccContactEmail = vendor.AccContactEmail; // update vendor name
                objFromDb.RegVendor = vendor.RegVendor; // update vendor name




                //_db.SaveChanges();
            }
        }
    }
}
