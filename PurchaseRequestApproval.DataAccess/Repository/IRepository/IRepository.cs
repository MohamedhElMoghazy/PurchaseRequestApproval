using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository.IRepository
{
   public  interface IRepository<T> where T: class // Because it is generic interface repository 
    {
        T Get(int Id); // get the class with the matched ID

        // to get a list of items with specific category 
        IEnumerable<T> GetAll(
     Expression<Func<T, bool>> filter = null,
     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
     string includeProperties = null
     );


        // to get a list of items header 
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null, 
            string includeProperties = null
            );

        // method to add an entity
        void Add(T entity);

        // method to remove any entity with ID
        void Remove(int id);
     
        // to remvoe entity with specic properties 
        void Remove(T entity);

        // to remvoe entity range of entities 
        void RemoveRange(IEnumerable <T> entity);





    }
}
