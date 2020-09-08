using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository.IRepository
{
   public  interface IPRApprovalRepository : IRepository<PRApproval>
    {
        void Update(PRApproval prapproval);


    }
}
