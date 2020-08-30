using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository.IRepository
{
   public  interface IEmployeeRepository : IRepository<Employee>
    {
        void Update(Employee employee);


    }
}
