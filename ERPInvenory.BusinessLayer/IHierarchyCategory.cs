using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInvenory.BusinessLayer
{
   public interface IHierarchyCategory
    {
        IEnumerable<Category> GetCategories();

        Category GetCategoryById();
        IEnumerable<Category> GetParentCategoriesById( Guid id);

        IEnumerable<Guid> GetChildCategoriesById(Guid id);
        void PostCategory(Category category);
        void DeleteCategory(int id);
        void UpdateCategory(Category category);


        void Dispose();
    }
}
