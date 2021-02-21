using CustomerNotification.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerNotificaton.Services.Interface
{
    public interface IMessageFormatter
    {
        string MessageTypeFormatter(UserModel userRequestInput);
    }
}
