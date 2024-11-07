using System.Linq.Expressions;

namespace sacmy.Client.Pages.Components.CustomDataGrid
{
    public class ColumnDefinition<T>
    {
        public Expression<Func<T, object>> Property { get; set; }
        public Func<T, object> PropertyCompiled => _propertyCompiled ??= Property.Compile();
        private Func<T, object> _propertyCompiled;

        public string Title { get; set; }
        public Func<T, string> Formatter { get; set; }
        public bool Sort { get; set; } = true; // Enable sorting by default
    }

}
