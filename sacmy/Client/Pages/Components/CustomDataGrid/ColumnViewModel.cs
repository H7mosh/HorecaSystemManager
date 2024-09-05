using System.Linq.Expressions;

namespace sacmy.Client.Pages.Components.CustomDataGrid
{
    public class ColumnDefinition<TItem>
    {
        public Expression<Func<TItem, object>> Property { get; set; }
        public string Title { get; set; }
    }
}
