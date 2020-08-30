using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
   public  class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Shipping = new ShippingRepository(_db);
            PurchaseType = new PurchaseTypeRepository(_db);
            SP_Call = new SP_Call(_db);


        }

        public IShippingRepository Shipping { get; private set; }
        public IPurchaseTypeRepository PurchaseType { get; private set; }
        public ISP_Call SP_Call { get; private set; }

        SP_Call IUnitOfWork.SP_Call => throw new NotImplementedException();

        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();

        }
    }
}
