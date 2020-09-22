using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class PurchaseTypeRepository : Repository<PurchaseType>, IPurchaseTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public PurchaseTypeRepository(ApplicationDbContext db): base (db)
        {
            _db = db;
        }

        public void Update(PurchaseType purchaseType)
        {
            var objFromDb = _db.PurchaseTypes.FirstOrDefault(s=>s.Id== purchaseType.Id);
            if (objFromDb != null)
            {
                objFromDb.PurcahseTypeName = purchaseType.PurcahseTypeName;
                objFromDb.PurcahseCode = purchaseType.PurcahseCode;
                objFromDb.PurchaseTypeAppr= purchaseType.PurchaseTypeAppr;
                //_db.SaveChanges();
            }
        }
    }
}
