using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository.IRepository
{
   public  interface IVendorContactRepository : IRepository<VendorContact>
    {
        void Update(VendorContact vendorcontact);


    }
}
