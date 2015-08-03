using ERPInvetnory.DataLayer;
using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInvetnory.BusinessLayer
{
    public class Category : ICategory
    {
        IUnitOfWork _unitOfWork =null;

        public Category(IUnitOfWork unitOfWork)
        {
            _unitOfWork =  unitOfWork;
        }
        public IEnumerable<inv_Category> GetCategories()
        {
            return _unitOfWork.Repository<inv_Category>().Get();
        }

        public inv_Category GetCategoryById()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<inv_Category> GetParentCategoriesById(Guid id)
        {
            return _unitOfWork.Repository<inv_Category>().SQLQuery("GetParentCategoryByID @ID",
                    new SqlParameter("ID", id)
                    ).AsEnumerable();
        }

        public IEnumerable<Guid> GetChildCategoriesById(Guid id )
        {
            return _unitOfWork.Repository<inv_Category>().SQLQuery("GetSubCategoryByParent @ID",
                  new SqlParameter("ID", id)
                  ).Select(s => s.CategoryId).AsEnumerable();
        }

        public void PostCategory(inv_Category category)
        {
            _unitOfWork.Repository<inv_Category>().Insert(category);
            _unitOfWork.Save();
        }

        public void DeleteCategory(int id)
        {
            _unitOfWork.Repository<inv_Category>().Delete(id);
            _unitOfWork.Save();
        }

        public void UpdateCategory(inv_Category category)
        {
            _unitOfWork.Repository<inv_Category>().Update(category);
            _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
