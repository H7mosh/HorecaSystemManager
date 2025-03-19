using Microsoft.AspNetCore.Components.Web;
using System.Linq.Expressions;

namespace sacmy.Client.Components.CustomDataGrid
{
    public class ColumnDefinition<TItem>
    {
        public Expression<Func<TItem, object>> Property { get; set; }
        public string Title { get; set; }
        public Func<TItem, string> Formatter { get; set; }
        public bool Sortable { get; set; } = true;
        public bool IsStickyNoteColumn { get; set; } = false;
    }

    public class DataGridRowClickEventArgs<T>
    {
        public T Item { get; set; }
        public MouseEventArgs MouseEventArgs { get; set; }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

}
