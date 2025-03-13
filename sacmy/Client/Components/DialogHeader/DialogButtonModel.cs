using Microsoft.AspNetCore.Components;

namespace sacmy.Client.Shared.Components.DialogHeader
{
    public class DialogButtonModel
    {
        public string Text { get; set; } = string.Empty;
        public string Color { get; set; } = "primary"; // default color
        public EventCallback OnClick { get; set; }
        public bool IsVisible { get; set; } = true;
        public string Icon { get; set; } = string.Empty; // Optional icon class
        public string CssClass { get; set; } = string.Empty; // Additional CSS classes
    }
}
