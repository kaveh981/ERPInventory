using ERPInventory.Model.BindingModels;
using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInventory.BusinessLayer
{
    public interface ICategory
    {

        IEnumerable<categoryNode> GetChildsByCategoryId(Guid? id);
        IEnumerable<inv_Category> GetParentsByCategoryId(Guid id);
        PagedResult<inv_Category> GetDescendentByCategoryId(Guid id,int start,int number);
        PagedResult<object> FilterCategories(FilterCategories filterCategories, int start, int number);
        void PostCategory(inv_Category category);
        void DeleteCategory(Guid id);
        void UpdateCategory(inv_Category category);

        void MoveAndSortCategory(CategoryDataSorting data);
        void Dispose();
    }
}
