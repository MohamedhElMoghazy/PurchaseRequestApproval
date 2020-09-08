using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class VendorContactRepository : Repository<VendorContact>, IVendorContactRepository
    {
        private readonly ApplicationDbContext _db;

        public VendorContactRepository(ApplicationDbContext db): base (db)
        {
            _db = db;
        }

        public void Update(VendorContact vendorcontact)
        {
            var objFromDb = _db.VendorContacts.FirstOrDefault(s=>s.Id== vendorcontact.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = vendorcontact.Name; // update vendorcontact name
                objFromDb.Email = vendorcontact.Email; // update vendorcontact Email 
                objFromDb.Phone = vendorcontact.Phone; // update vendorcontact Phone 
                objFromDb.Branch = vendorcontact.Branch; // update vendorcontact Branch 
                objFromDb.Remark = vendorcontact.Remark; // update vendorcontact Remark     
                objFromDb.VendorId = vendorcontact.VendorId; // update vendorcontact VendorId 

                //_db.SaveChanges();
            }
        }
    }
}
