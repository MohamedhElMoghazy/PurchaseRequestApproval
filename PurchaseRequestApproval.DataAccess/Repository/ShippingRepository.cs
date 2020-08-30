using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class ShippingRepository : Repository<Shipping>, IShippingRepository
    {
        private readonly ApplicationDbContext _db;
        public ShippingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }

        public void Update(Shipping shipping)
        {
            var objFromDb = _db.Shippings.FirstOrDefault(s=>s.Id==shipping.Id);
            if(objFromDb != null )
            { 
             objFromDb.ShippingName = shipping.ShippingName;
             objFromDb.ShippingDescription = shipping.ShippingDescription;
             objFromDb.ShippingACC = shipping.ShippingACC;

                // _db.SaveChanges();
            }
        }
    }
}
