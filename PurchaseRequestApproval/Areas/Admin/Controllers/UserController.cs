using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using PurchaseRequestApproval.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PurchaseRequestApproval.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin_Modify + "," + SD.Role_Admin_View)]// Add authorization Level

    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db; // here we work with DB Context to show the ability to use DB Context
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

       



        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
         {

            var userList = _db.ApplicationUsers.Include(u=>u.Employee).ToList(); 
            var userRole = _db.UserRoles.ToList(); // to show all users role
            var roles = _db.Roles.ToList(); // to show all roles

            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
                if (user.Employee==null)
                {
                    user.Employee = new Employee()
                    {
                        EmployeeName = ""


                    };


                }
                    
            }



            //var allObj = _unitOfWork.SP_Call.List<Employee>(SD.Proc_Employee_GetAll, null); // to allow stored procedure
            return Json(new { data = userList });


            /*
                    public async Task<IActionResult> GetAll()
                        {
                            var allObj = await _unitOfWork.Employee.GetAllAsync();
                            return Json(new { data = allObj });
                        }
            */


        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked, we will unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Operation Successful." });
        }


        #endregion


    }
}
