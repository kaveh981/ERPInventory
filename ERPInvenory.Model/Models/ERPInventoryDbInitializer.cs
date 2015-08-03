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
            StringBuilder AllSubCategoryId = new StringBuilder();
            AllSubCategoryId.Append(" create FUNCTION [dbo].[AllSubCategoryId](@CategoryId uniqueIdentifier )");
            AllSubCategoryId.Append(" RETURNS TABLE");
            AllSubCategoryId.Append(" AS");
            AllSubCategoryId.Append(" RETURN (WITH CategoryChart (CategoryId,Cat_ParentId,CategoryLevel) AS");
            AllSubCategoryId.Append(" (");
            AllSubCategoryId.Append(" SELECT");
            AllSubCategoryId.Append(" a.CategoryId,  a.Cat_ParentId,0");
            AllSubCategoryId.Append(" FROM dbo.inv_Category as a");
            AllSubCategoryId.Append(" WHERE a.CategoryId 	= @CategoryId");
            AllSubCategoryId.Append(" UNION ALL");
            AllSubCategoryId.Append(" SELECT");
            AllSubCategoryId.Append(" a.CategoryId,  a.Cat_ParentId, b.Categorylevel+1");
            AllSubCategoryId.Append(" FROM dbo.inv_Category a");
            AllSubCategoryId.Append(" INNER JOIN CategoryChart b ON a.Cat_ParentId = b.CategoryId");
            AllSubCategoryId.Append(" ) select CategoryId from CategoryChart );");
            context.Database.ExecuteSqlCommand(AllSubCategoryId.ToString());

            StringBuilder GetFullPathCategory = new StringBuilder();
            GetFullPathCategory.Append(" create FUNCTION [dbo].[GetFullPathCategory](@Categoryid uniqueidentifier )");
            GetFullPathCategory.Append(" RETURNS TABLE");
            GetFullPathCategory.Append(" AS");
            GetFullPathCategory.Append(" RETURN (WITH Categorychart ( CategoryID, Cat_CreateTime,Cat_NodeDepth,Cat_ParentId,Cat_Priority,Cat_StampTime,Cat_Title,Categorylevel) AS");
            GetFullPathCategory.Append(" (");
            GetFullPathCategory.Append(" SELECT");
            GetFullPathCategory.Append("     a.CategoryID, a.Cat_CreateTime,a.Cat_NodeDepth,a.Cat_ParentId,a.Cat_Priority,a.Cat_StampTime,a.Cat_Title,0");
            GetFullPathCategory.Append("   FROM dbo.inv_Category as a");
            GetFullPathCategory.Append("   WHERE a.CategoryID 	= @CategoryID");
            GetFullPathCategory.Append("  UNION ALL");
            GetFullPathCategory.Append("  SELECT");
            GetFullPathCategory.Append("     a.CategoryID, a.Cat_CreateTime,a.Cat_NodeDepth,a.Cat_ParentId,a.Cat_Priority,a.Cat_StampTime,a.Cat_Title, b.CategoryLevel+1");
            GetFullPathCategory.Append("   FROM dbo.inv_Category a");
            GetFullPathCategory.Append("     INNER JOIN CategoryChart b ON a.CategoryID = b.Cat_ParentId");
            GetFullPathCategory.Append(")");
            GetFullPathCategory.Append(" select * from Categorychart   );");
            context.Database.ExecuteSqlCommand(GetFullPathCategory.ToString());

            StringBuilder GetParentCategoryByID = new StringBuilder();
            GetParentCategoryByID.Append("  create proc [dbo].[GetParentCategoryByID]");
            GetParentCategoryByID.Append(" @ID uniqueidentifier,");
            GetParentCategoryByID.Append(" @startLevel nvarchar(50)=null");
            GetParentCategoryByID.Append(" as");
            GetParentCategoryByID.Append(" if(@startLevel ='' or @startLevel is null or @startLevel='0')");
            GetParentCategoryByID.Append(" set @startLevel=null; ");
            GetParentCategoryByID.Append(" select * into #temp from dbo.GetFullPathCategory(@ID)");
            GetParentCategoryByID.Append(" if(@startLevel is null)");
            GetParentCategoryByID.Append(" select * from #temp order by CategoryLevel desc");
            GetParentCategoryByID.Append(" else");
            GetParentCategoryByID.Append(" select * from #temp where CategoryLevel<(select CategoryLevel from #temp where Category=@startLevel) order by CategoryLevel desc ");
            GetParentCategoryByID.Append(" drop table #temp");
            context.Database.ExecuteSqlCommand(GetParentCategoryByID.ToString());

            StringBuilder GetSubCategoryByParent = new StringBuilder();
            GetSubCategoryByParent.Append("   create proc [dbo].[GetSubCategoryByParent]");
            GetSubCategoryByParent.Append(" @Cat_ParentId uniqueidentifier");
            GetSubCategoryByParent.Append(" as");
            GetSubCategoryByParent.Append(" SELECT     CategoryId");
            GetSubCategoryByParent.Append(" FROM         Categories");
            GetSubCategoryByParent.Append(" where Cat_ParentId in( select * from dbo.AllSubCategoryID(@Cat_ParentId))");
            context.Database.ExecuteSqlCommand(GetSubCategoryByParent.ToString());

            inv_Category cat1, cat12, cat13, cat14, cat131, cat132;

            IList<inv_Category> categories1 = new List<inv_Category>();
            IList<inv_Category> categories2 = new List<inv_Category>();

            cat1 = new inv_Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category1", Cat_Priority = 1, Cat_NodeDepth = 0, Categories = categories1 };
            cat12 = new inv_Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category12", Cat_NodeDepth = 1, Cat_Priority = 1 };
            cat13 = new inv_Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category13", Cat_NodeDepth = 1, Cat_Priority = 2, Categories = categories2 };
            cat14 = new inv_Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category14", Cat_NodeDepth = 1, Cat_Priority = 3 };
            categories1.Add(cat12);
            categories1.Add(cat13);
            categories1.Add(cat14);
            cat131 = new inv_Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category131", Cat_NodeDepth = 2, Cat_Priority = 1 };
            cat132 = new inv_Category() { Cat_CreateTime = DateTime.Now, Cat_Title = "Category132", Cat_NodeDepth = 2, Cat_Priority = 2 };
            categories2.Add(cat131);
            categories2.Add(cat132);
            context.Categories.Add(cat1);

            base.Seed(context);
        }

    }
}
