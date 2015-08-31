using ERPInventory.DataLayer;
using ERPInventory.Model.BindingModels;
using ERPInventory.Model.Models;
using ERPInventory.Model.SqlQueryResultModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ERPInventory.BusinessLayer
{
    public class Category : ICategory
    {
        IUnitOfWork _unitOfWork = null;

        public Category(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<inv_Category> GetParentByCategoryId(Guid id)
        {
            return _unitOfWork.Repository<inv_Category>().SQLQuery("GetParentCategoryByID @ID",
                    new SqlParameter("ID", id)
                    ).AsEnumerable();
        }

        public IEnumerable<categoryNode> GetChildsByCategoryId(Guid? id)
        {
            return _unitOfWork.Repository<inv_Category>().Get(s => s.Cat_ParentId == id, o => o.OrderBy(ob => ob.Cat_Priority)).Results.Select(s => new categoryNode() { id = s.CategoryId, title = s.Cat_Title + "--" + Convert.ToString(s.Cat_Priority), nodes = Enumerable.Empty<categoryNode>() });
        }


        public IEnumerable<inv_Category> GetParentsByCategoryId(Guid id)
        {
            return _unitOfWork.Repository<inv_Category>().SQLQuery("GetParentCategoryByID @ID",
                    new SqlParameter("ID", id)
                    ).AsEnumerable();
        }

        public PagedResult<inv_Category> GetDescendentByCategoryId(Guid id,int start,int number)
        {
            var ids = _unitOfWork.Repository<DescendentTree>().SQLQuery("GetSubCategoryByParent @ID",
                  new SqlParameter("ID", id)
                  ).Select(s => s.CategoryId).ToList();
         return  _unitOfWork.Repository<inv_Category>().Get(c => ids.Contains(c.CategoryId),o=>o.OrderBy(ob=>ob.Cat_Title),null,start,number);

        }

        public void PostCategory(inv_Category category)
        {
            category.Cat_CreateTime = DateTime.Now;
            _unitOfWork.Repository<inv_Category>().Insert(category);
            _unitOfWork.Save();
        }

        public void DeleteCategory(Guid id)
        {
            inv_Category category = _unitOfWork.Repository<inv_Category>().GetById(i => i.CategoryId == id);
            Guid? parentId = category.Cat_ParentId;
            _unitOfWork.Repository<inv_Category>().Delete(category);
            _unitOfWork.Save();
        }

        public void UpdateCategory(inv_Category category)
        {
            var cat= _unitOfWork.Repository<inv_Category>().GetById(s => s.CategoryId == category.CategoryId);
            cat.Cat_Title = category.Cat_Title;
            cat.Cat_StampTime = DateTime.Now;
            _unitOfWork.Repository<inv_Category>().Update(cat);
            _unitOfWork.Save();
        }

        public void MoveAndSortCategory(CategoryDataSorting data)
        {
            inv_Category category = _unitOfWork.Repository<inv_Category>().GetById(s => s.CategoryId == data.id);
            var oldCat = category;
            Guid? oldParentId = category.Cat_ParentId;
            category.Cat_ParentId = data.parentId;
            category.Cat_StampTime = DateTime.Now;
            _unitOfWork.Repository<inv_Category>().Update(category);
            var destCats = _unitOfWork.Repository<inv_Category>().Get(s => s.Cat_ParentId == data.parentId).Results.ToList();
            destCats.Add(category);
            SortCategories(destCats, data.destOrder);
            _unitOfWork.Save();
        }

        private void SortCategories(List<inv_Category> categories, List<Guid> ids)
        {
            int p = 1;
            if (ids.Count>0)
            {

                foreach (var id in ids)
                {
                    var category = categories.Find(i => i.CategoryId == id);
                    category.Cat_Priority = p;
                    p++;
                    _unitOfWork.Repository<inv_Category>().Update(category);
                }
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
