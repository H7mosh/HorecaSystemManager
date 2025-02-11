namespace sacmy.Client.Shared.Toast
{
    public class ToastService
    {
        public event Action OnToastsUpdated;
        private List<ToastInstance> _toasts = new();

        public IEnumerable<ToastInstance> Toasts => _toasts.AsEnumerable();

        public void ShowToast(string message, string type = "primary")
        {
            var toast = new ToastInstance
            {
                Id = Guid.NewGuid(),
                Message = message,
                Type = type
            };

            _toasts.Add(toast);

            // Schedule toast removal after 3 seconds
            Task.Delay(3000).ContinueWith(_ => RemoveToast(toast));

            NotifyStateChanged();
        }

        public void RemoveToast(ToastInstance toast)
        {
            if (_toasts.Contains(toast))
            {
                _toasts.Remove(toast);
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnToastsUpdated?.Invoke();
    }

    public class ToastInstance
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
