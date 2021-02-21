using CustomerNotification.Common.Models;
using CustomerNotificaton.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CustomerNotificaton.Services.Implementation
{
    public class XMLMessageFormatter : IMessageFormatter
    {
        public string MessageTypeFormatter(UserModel userRequestInput)
        {
            var responseString = string.Empty;
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(userRequestInput.GetType());
                serializer.Serialize(stringwriter, userRequestInput);

                responseString = stringwriter.ToString();
            }

            return responseString;
        }
    }
}
