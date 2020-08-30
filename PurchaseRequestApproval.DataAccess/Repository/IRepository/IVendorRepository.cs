using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository.IRepository
{
   public  interface IVendorRepository : IRepository<Vendor>
    {
        void Update(Vendor vendor);

    }
}
