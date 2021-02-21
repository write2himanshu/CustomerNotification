using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerNotification.Common.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public Channel ChannelRequestType { get; set; }
    }

    public enum Channel
    {
        JSON = 1,
        XML = 2
    }
}
