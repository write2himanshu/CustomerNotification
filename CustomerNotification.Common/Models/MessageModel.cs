using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerNotification.Common.Models
{
    public enum MessageType
    {
        NewUserRegistered = 1,
        UserDeleted = 2,
        UserAccessBlocked = 3
    }
}
