using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace ERPInventory.Model.Models
{
    public class ERPInventoryDbInitializer : DropCreateDatabaseIfModelChanges<ERPInventoryDBContext>
    {
        protected override void Seed(ERPInventoryDBContext context)
        {

//            context.Database.ExecuteSqlCommand("create FUNCTION [dbo].[AllSubCategoryId](@CategoryId uniqueIdentifier )
//RETURNS TABLE
//AS
//RETURN (WITH CategoryChart (CategoryId,Cat_ParentId,CategoryLevel) AS
// (
//  SELECT
//     a.CategoryId,  a.Cat_ParentId,0
//   FROM dbo.Categories as a
//   WHERE a.CategoryId 	= @CategoryId
//  UNION ALL
//  SELECT
//     a.CategoryId,  a.Cat_ParentId, b.Categorylevel+1
//   FROM dbo.Categories a
//     INNER JOIN CategoryChart b ON a.Cat_ParentId = b.CategoryId
//) select CategoryId from CategoryChart );
//");
            Category cat1, cat12, cat13, cat14, cat131, cat132;

            IList<Category> categories1 = new List<Category>();
            IList<Category> categories2 = new List<Category>();

            cat1 = new Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category1", Cat_Priority = 1, Cat_NodeDepth = 0, Categories=categories1};
            cat12 = new Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category12", Cat_NodeDepth = 1, Cat_Priority = 1 };
            cat13 = new Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category13", Cat_NodeDepth = 1, Cat_Priority = 2, Categories=categories2};
            cat14 = new Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category14", Cat_NodeDepth = 1, Cat_Priority = 6 };
            categories1.Add(cat12);
            categories1.Add(cat13);
            categories1.Add(cat14);
            cat131 = new Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category131", Cat_NodeDepth = 2, Cat_Priority = 1};
            cat132 = new Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category132", Cat_NodeDepth = 2, Cat_Priority = 2 };
            categories2.Add(cat131);
            categories2.Add(cat132);
            //object parameters=null;
            //object[] parameterArray = (object[])parameters;
            context.Categories.Add(cat1);

            base.Seed(context);
        }

    }
}
