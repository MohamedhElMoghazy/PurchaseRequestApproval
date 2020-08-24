using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurchaseRequestApproval.Models;

namespace PurchaseRequestApproval.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // add the shipping method to the database
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<PurchaseType> PurchaseTypes { get; set; }
    }
}
