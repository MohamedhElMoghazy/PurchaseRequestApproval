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
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<VendorContact> VendorContacts { get; set; }
        public DbSet<PRApproval> PRApprovals { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<PRAQuote> PRAQuotes { get; set; }



    }
}
