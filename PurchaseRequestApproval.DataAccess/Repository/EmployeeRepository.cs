using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db): base (db)
        {
            _db = db;
        }

        public void Update(Employee employee)
        {
            var objFromDb = _db.Employees.FirstOrDefault(s=>s.Id== employee.Id);
            if (objFromDb != null)
            {
                objFromDb.EmployeeName = employee.EmployeeName; // update employee name
                objFromDb.EmployeeCode = employee.EmployeeCode; // update employee code
                objFromDb.EmployeePosition = employee.EmployeePosition; // update employee position
                objFromDb.EmployeePhone = employee.EmployeePhone; // update employee phone
                objFromDb.EmployeeEmail = employee.EmployeeEmail; // update employee email
                objFromDb.EmployeeSite = employee.EmployeeSite; // update employee site
                




                //_db.SaveChanges();
            }
        }
    }
}
