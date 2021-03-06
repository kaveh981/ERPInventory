﻿using ERPInventory.DataLayer;
using ERPInventory.Model.BindingModels;
using ERPInventory.Model.Models;
using ERPInventory.Model.SqlQueryResultModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
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
            return _unitOfWork.Repository<inv_Category>().Get(s => s.Cat_ParentId == id, o => o.OrderBy(ob => ob.Cat_Priority), 0, 0, i => i.Categories)
                .Results.Select(s => new categoryNode()
                {
                    id = s.CategoryId,
                    name = s.Cat_Title + "--" + Convert.ToString(s.Cat_Priority),
                    hasChildren = s.Categories.Count > 0 ? true : false,
                    nodes = Enumerable.Empty<categoryNode>()
                });
        }


        public IEnumerable<inv_Category> GetParentsByCategoryId(Guid id)
        {
            return _unitOfWork.Repository<inv_Category>().SQLQuery("GetParentCategoryByID @ID",
                    new SqlParameter("ID", id)
                    ).AsEnumerable();
        }

        public PagedResult<inv_Category> GetDescendentByCategoryId(Guid id, int start, int number)
        {

            var ids = _unitOfWork.Repository<DescendentTree>().SQLQuery("GetSubCategoryByParent @ID",
                  new SqlParameter("ID", id)
                  ).Select(s => s.CategoryId).ToList();
            return _unitOfWork.Repository<inv_Category>().Get(c => ids.Contains(c.CategoryId), o => o.OrderBy(ob => ob.Cat_Title), start, number, null);

        }

        public PagedResult<object> FilterCategories(FilterCategories filterCategories, int start, int number)
        {

            var predicate = PredicateBuilder.True<inv_Category>();
            //Expression<Func<inv_Category, bool>> clientWhere = c => true;

            if (!string.IsNullOrEmpty(filterCategories.title))
            {
                predicate = predicate.And(c => c.Cat_Title.Contains(filterCategories.title));
                //var prefix = clientWhere.Compile();
                //clientWhere = c => prefix(c) && c.Cat_Title.Contains(filterCategories.title);
            }
            if (!string.IsNullOrEmpty(filterCategories.parentId))
            {
                var ids = _unitOfWork.Repository<DescendentTree>().SQLQuery("GetSubCategoryByParent @ID",
                  new SqlParameter("ID", Guid.Parse(filterCategories.parentId))
                  ).Select(s => s.CategoryId).ToList();
                predicate = predicate.And(c => ids.Contains(c.CategoryId));
            }

            if (filterCategories.createTime!=null)
            {
                if (filterCategories.createTime.from != null)
                {
                    predicate = predicate.And(c => c.Cat_CreateTime > filterCategories.createTime.from);
                }
                if (filterCategories.createTime.to != null)
                {
                    predicate = predicate.And(c => c.Cat_CreateTime < filterCategories.createTime.to);
                }
            }
            if (filterCategories.nodeDepth != null)
            {
                if (filterCategories.nodeDepth.from != null)
                {
                    predicate = predicate.And(c => c.Cat_NodeDepth > filterCategories.nodeDepth.from);
                }
                if (filterCategories.nodeDepth.to != null)
                {
                    predicate = predicate.And(c => c.Cat_NodeDepth < filterCategories.nodeDepth.to);
                }
            }
            if (filterCategories.priority != null)
            {
                if (filterCategories.priority.from != null)
                {
                    predicate = predicate.And(c => c.Cat_Priority > filterCategories.priority.from);
                }
                if (filterCategories.priority.to != null)
                {
                    predicate = predicate.And(c => c.Cat_Priority < filterCategories.priority.to);
                }
            }
            if (filterCategories.stampTime != null)
            {
                if (filterCategories.stampTime.from != null)
                {
                    predicate = predicate.And(c => c.Cat_StampTime > filterCategories.stampTime.from);
                }
                if (filterCategories.stampTime.to != null)
                {
                    predicate = predicate.And(c => c.Cat_StampTime < filterCategories.stampTime.to);
                }
            }

            Func<IQueryable<inv_Category>, IOrderedQueryable<inv_Category>> orderBy = o => o.OrderBy(ob => ob.Cat_Title);
            bool reverse = filterCategories.sortBy.reverse;
            if (!string.IsNullOrEmpty(filterCategories.sortBy.predicate))
            {
                switch (filterCategories.sortBy.predicate.ToLower())
                {
                    default:
                        orderBy = o => o.OrderBy(ob => ob.Cat_Title);
                        break;
                    case "title":
                        if (!reverse)
                            orderBy = o => o.OrderBy(ob => ob.Cat_Title);
                        else
                            orderBy = o => o.OrderByDescending(ob => ob.Cat_Title);
                        break;
                    case "nodeDepth":
                        if (!reverse)
                            orderBy = o => o.OrderBy(ob => ob.Cat_NodeDepth);
                        else
                            orderBy = o => o.OrderByDescending(ob => ob.Cat_NodeDepth);
                        break;
                    case "createTime":
                        if (!reverse)
                            orderBy = o => o.OrderBy(ob => ob.Cat_CreateTime);
                        else
                            orderBy = o => o.OrderByDescending(ob => ob.Cat_CreateTime);
                        break;
                    case "priority":
                        if (!reverse)
                            orderBy = o => o.OrderBy(ob => ob.Cat_Priority);
                        else
                            orderBy = o => o.OrderByDescending(ob => ob.Cat_Priority);
                        break;
                    case "stampTime":
                        if (!reverse)
                            orderBy = o => o.OrderBy(ob => ob.Cat_StampTime);
                        else
                            orderBy = o => o.OrderByDescending(ob => ob.Cat_StampTime);
                        break;

                }
            }
            var result = _unitOfWork.Repository<inv_Category>().Get(predicate, orderBy, start, number, i => i.Parent);

            return new PagedResult<object>() { CurrentPage = result.CurrentPage, PageCount = result.PageCount, PageSize = result.PageSize, RowCount = result.RowCount, Results = result.Results.Select(s => new { s.Cat_CreateTime, s.Cat_NodeDepth, s.Cat_Priority, s.Cat_StampTime, s.Cat_Title, ParentCat_Title = s.Parent != null ? s.Parent.Cat_Title : "" }) };

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
            var cat = _unitOfWork.Repository<inv_Category>().GetById(s => s.CategoryId == category.CategoryId);
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
            if (ids.Count > 0)
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


        //private UserListItem SortUsers(object users, string sort)
        //{
        //    switch (sort.ToLower())
        //    {
        //        default:
        //            users = users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
        //            break;
        //        case "namedesc":
        //            users = users.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName);
        //            break;
        //        case "emailasc":
        //            users = users.OrderBy(u => u.Email);
        //            break;
        //        case "emaildesc":
        //            users = users.OrderByDescending(u => u.Email);
        //            break;
        //        case "typeasc":
        //            users = users.OrderBy(u => u.UsertypeSortingOrder);
        //            break;
        //        case "typedesc":
        //            users = users.OrderByDescending(u => u.UsertypeSortingOrder);
        //            break;
        //    }
        //    return users;
        //}
        //public static Expression<inv_Category> AndAlso<TDelegate>(this Expression<inv_Category> left, Expression<inv_Category> right)
        //{
        //    return Expression.Lambda<inv_Category>(Expression.AndAlso(left, right), left.Parameters);
        //}


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
