using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInvetnory.BusinessLayer
{
    public interface ICategory
    {

        IEnumerable<inv_Category> GetChildsByCategoryId(Guid id);
        IEnumerable<inv_Category> GetParentsByCategoryId(Guid id);
        IEnumerable<Guid> GetDescendentByCategoryId(Guid id);

        void PostCategory(inv_Category category);
        void DeleteCategory(int id);
        void UpdateCategory(inv_Category category);


        void Dispose();
    }
}
