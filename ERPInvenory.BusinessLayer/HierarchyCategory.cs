using ERPInvenory.DataLayer;
using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInvenory.BusinessLayer
{
    public class HierarchyCategory : IHierarchyCategory
    {
        IUnitOfWork _unitOfWork =null;

        public HierarchyCategory(IUnitOfWork unitOfWork)
        {
            _unitOfWork =  unitOfWork;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Repository<Category>().Get();
        }

        public Category GetCategoryById()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetParentCategoriesById(Guid id)
        {
            return _unitOfWork.Repository<Category>().SQLQuery("GetParentCategoryByID @ID",
                    new SqlParameter("ID", id)
                    ).AsEnumerable();
        }

        public IEnumerable<Guid> GetChildCategoriesById(Guid id )
        {
            return _unitOfWork.Repository<Category>().SQLQuery("GetSubCategoryByParent @ID",
                  new SqlParameter("ID", id)
                  ).Select(s => s.CategoryId).AsEnumerable();
        }

        public void PostCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Insert(category);
            _unitOfWork.Save();
        }

        public void DeleteCategory(int id)
        {
            _unitOfWork.Repository<Category>().Delete(id);
            _unitOfWork.Save();
        }

        public void UpdateCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Update(category);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
