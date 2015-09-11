using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInventory.Model.BindingModels
{
    public class categoryNode
    {
        public Guid id { get; set; }
        public string name { get; set; }

        public bool hasChildren { get; set; }
        public IEnumerable<categoryNode> nodes { get; set; }
    }

    public class CategoryDataSorting
    {
        public Guid id { get; set; }
        public Nullable<Guid> parentId { get; set; }
        public List<Guid> destOrder { get; set; }
    }

    public class FilterCategories
    {
        public Nullable<int> start { get; set; }
        public Nullable<int> number { get; set; }
        public string parentId { get; set; }
        public string title { get; set; }
        public rangeSelector<int?> nodeDepth { get; set; }
        public rangeSelector<DateTime> createTime { get; set; }
        public rangeSelector<int?> priority { get; set; }
        public rangeSelector<DateTime> stampTime { get; set; }
        public Sorting sortBy { get; set; }
    }

    public class rangeSelector<T>
    {
        public T from { get; set; }
        public T to { get; set; }
    }

    public class Sorting
    {
        public string predicate { get; set; }
        public bool reverse { get; set; }
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }
}
