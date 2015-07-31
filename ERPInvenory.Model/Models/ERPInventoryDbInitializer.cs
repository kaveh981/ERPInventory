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
            IList<Category> defualtcategories = new List<Category>();
            Category cat1, cat12, cat13, cat14, cat131, cat132;

            IList<Category> categories1 = new List<Category>();
            IList<Category> categories13 = new List<Category>();



            cat12 = new Category() { Cat_Title = "Category12", Cat_NodeDepth = 0 };
            cat13 = new Category() { Cat_Title = "Category13",  Cat_NodeDepth = 0, Categories=categories13 };
            cat14 = new Category() { Cat_Title = "Category14",  Cat_NodeDepth = 0 };


            cat131 = new Category() { Cat_Title = "Category131", Cat_NodeDepth = 0 };
            cat132 = new Category() { Cat_Title = "Category132",  Cat_NodeDepth = 0 };

            categories1.Add(cat12);
            categories1.Add(cat13);
            categories1.Add(cat14);
            cat1 = new Category() { Cat_Title = "Category1", Cat_NodeDepth = 0, Categories = categories1 };

            categories13.Add(cat131);
            categories13.Add(cat132);

            context.Categories.Add(cat1);
           
            base.Seed(context);
        }

    }
}
