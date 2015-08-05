using ERPInventory.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInventory.Model.Models
{
    public class ERPInventoryDBContext: IdentityDbContext<ApplicationUser>
    {
        public ERPInventoryDBContext()
            : base("name=defaultConnectionString", throwIfV1Schema: false)
        {
            Database.SetInitializer<ERPInventoryDBContext>(new ERPInventoryDbInitializer());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ERPInventoryDBContext>().MapToStoredProcedures();
        //}

        public static ERPInventoryDBContext Create()
        {
            return new ERPInventoryDBContext();
        }

        public DbSet<inv_Category> Categories { get; set; }

    }
}
