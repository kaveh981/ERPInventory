using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace ERPInventory.Model.Models
{
    public class ERPInventoryDbInitializer : DropCreateDatabaseAlways<ERPInventoryDBContext>
    {
        protected override void Seed(ERPInventoryDBContext context)
        {
            Category cat1, cat12, cat13, cat14, cat131, cat132;

            cat1 = new Category() { Cat_Title = "Category1", Cat_NodeDepth = 0 };
            cat12 = new Category() { Cat_Title = "Category12", Cat_NodeDepth = 1,Parent=cat1 };
            cat13 = new Category() { Cat_Title = "Category13", Cat_NodeDepth = 1, Parent = cat1 };
            cat14 = new Category() { Cat_Title = "Category14", Cat_NodeDepth = 1, Parent = cat1 };
            cat131 = new Category() { Cat_Title = "Category131", Cat_NodeDepth = 2, Parent = cat13 };
            cat132 = new Category() { Cat_Title = "Category132", Cat_NodeDepth = 2, Parent = cat13 };

            context.Categories.Add(cat12);
            context.Categories.Add(cat14);
            context.Categories.Add(cat131);
            context.Categories.Add(cat132);
           
            base.Seed(context);
        }

    }
}
