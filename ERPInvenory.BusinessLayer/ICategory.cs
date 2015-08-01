using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInvenory.BusinessLayer
{
   public interface ICategory
    {
        IEnumerable<inv_Category> GetCategories();

        inv_Category GetCategoryById();
        IEnumerable<inv_Category> GetParentCategoriesById( Guid id);

        IEnumerable<Guid> GetChildCategoriesById(Guid id);
        void PostCategory(inv_Category category);
        void DeleteCategory(int id);
        void UpdateCategory(inv_Category category);


        void Dispose();
    }
}
