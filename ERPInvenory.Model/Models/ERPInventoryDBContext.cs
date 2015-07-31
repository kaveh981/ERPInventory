﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInventory.Model.Models
{
    public class ERPInventoryDBContext:DbContext
    {
        public ERPInventoryDBContext()
            : base("name=defaultConnectionString")
        {
            Database.SetInitializer<ERPInventoryDBContext>(new ERPInventoryDbInitializer());
        }


        public DbSet<Category> Categories { get; set; }

    }
}
