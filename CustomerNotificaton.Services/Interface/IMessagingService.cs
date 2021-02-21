using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomerNotificaton.Services.Interface
{
    public interface IMessagingService
    {
        public Task SendMessageAsync(string customerId, string messageBody);
    }
}
