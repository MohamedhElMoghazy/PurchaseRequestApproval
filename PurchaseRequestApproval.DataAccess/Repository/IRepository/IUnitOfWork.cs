using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IShippingRepository Shipping { get;  }
        IPurchaseTypeRepository PurchaseType { get; }
        IVendorRepository Vendor { get; }
        IEmployeeRepository Employee { get; }

        IPRApprovalRepository PRApproval { get; }
        IPRAQuoteRepository PRAQuote { get; }
        IProjectRepository Project { get; }
        IQuoteRepository Quote { get; }
        IVendorContactRepository VendorContact { get; }


        //SP_Call SP_Call  { get; }
        ISP_Call SP_Call { get; }
        void Save();

    }
}
