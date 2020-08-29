using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IShippingRepository Shipping { get;  }
        IPurchaseTypeRepository PurchaseType { get; }
        SP_Call SP_Call  { get; }


    }
}
