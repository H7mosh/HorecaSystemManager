using sacmy.Shared.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.Core
{
    public class UserGlobalClass
    {
        public UserViewModel User { get; set; }
        public event EventHandler UserChangedEvent;

        public void SetUser(UserViewModel user)
        {
            User = user;
            UserChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
