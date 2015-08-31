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
        public string title { get; set; }
        public IEnumerable<categoryNode> nodes { get; set; }
    }

    public class CategoryDataSorting
    {
        public Guid id { get; set; }
        public Nullable<Guid> parentId { get; set; }
        public List<Guid> destOrder { get; set; }
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
