using System;
using System.Collections.Generic;
using System.Text;

namespace BililiveRecorder.Core
{
    public static class RoomNotifyEvent
    {
        public static event EventHandler NotifyEvent;

        public static void Notify(object sender, EventArgs args)
        {
            NotifyEvent?.Invoke(sender, args);
        }
    }
}
