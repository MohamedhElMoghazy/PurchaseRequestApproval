using PurchaseRequestApproval.DataAccess.Data;
using PurchaseRequestApproval.DataAccess.Repository.IRepository;
using PurchaseRequestApproval.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRequestApproval.DataAccess.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _db;

        public ProjectRepository(ApplicationDbContext db): base (db)
        {
            _db = db;
        }

        public void Update(Project project)
        {
            var objFromDb = _db.Projects.FirstOrDefault(s=>s.Id== project.Id);
            if (objFromDb != null)
            {
                objFromDb.ProjectName = project.ProjectName; // update project name
                objFromDb.WorkPackageIn = project.WorkPackageIn; // update project WorkPackageIn 
                objFromDb.ContractNo = project.ContractNo; // update project ContractNo 
               
                




                //_db.SaveChanges();
            }
        }
    }
}
