using CustomerNotification.Common.Models;
using CustomerNotificaton.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerNotificaton.Services.Implementation
{
    public class MessageGenerator : IMessageGenerator
    {
        public string MessageProcessor(UserModel userRequestInput, MessageType messageType)
        {
            string message = string.Empty;

            IMessageFormatter result = null;

            switch (userRequestInput.ChannelRequestType)
            {
                case Channel.JSON:
                    JSONMessageFormatter jFormat = new JSONMessageFormatter();
                    result = jFormat;
                    break;
                case Channel.XML:
                    XMLMessageFormatter xmlFormat = new XMLMessageFormatter();
                    result = xmlFormat;
                    break;
                default:
                    break;
            }

            message = result?.MessageTypeFormatter(userRequestInput);

            return message;
        }
    }
}
