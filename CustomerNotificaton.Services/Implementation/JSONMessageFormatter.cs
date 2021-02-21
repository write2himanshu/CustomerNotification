using CustomerNotification.Common.Models;
using CustomerNotificaton.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerNotificaton.Services.Implementation
{
    public class JSONMessageFormatter : IMessageFormatter
    {

        public string MessageTypeFormatter(UserModel userRequestInput)
        {
            string message = JsonConvert.SerializeObject(userRequestInput);
            return message;
        }
    }
}
