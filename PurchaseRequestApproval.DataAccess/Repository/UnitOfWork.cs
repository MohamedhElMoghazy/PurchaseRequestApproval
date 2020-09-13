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

            Vendor = new VendorRepository(_db);
            Employee = new EmployeeRepository(_db);

            PRApproval = new PRApprovalRepository(_db);
            PRAQuote = new PRAQuoteRepository(_db);
            Project= new ProjectRepository(_db);
            Quote = new QuoteRepository(_db);
            VendorContact = new VendorContactRepository(_db);

            ApplicationUser = new ApplicationUserRepository(_db);




            //SP_Call = new SP_Call(_db);
            SP_Call = new SP_Call(_db);



        }

        public IShippingRepository Shipping { get; private set; }
        public IPurchaseTypeRepository PurchaseType { get; private set; }

        public IVendorRepository Vendor { get; private set; }
        public IEmployeeRepository Employee { get; private set; }

        public IPRApprovalRepository PRApproval { get; private set; }
        public IPRAQuoteRepository PRAQuote { get; private set; }
        public IProjectRepository Project { get; private set; }
        public IQuoteRepository Quote { get; private set; }
        public IVendorContactRepository VendorContact { get; private set; }

// Arrange for Application User
        public IApplicationUserRepository ApplicationUser { get; private set; }




        //public ISP_Call SP_Call { get; private set; }
        public ISP_Call SP_Call { get; private set; }


        //SP_Call IUnitOfWork.SP_Call => throw new NotImplementedException();

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
