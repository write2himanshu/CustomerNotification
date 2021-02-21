using CustomerNotification.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CustomerNotification.Common
{
    public static class BaseService
    {
        public static void PostLogMessage(LoggingViewModel loggingViewModel, string loggingBaseUrl)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsync(loggingBaseUrl + "/logger", new StringContent(JsonConvert.SerializeObject(loggingViewModel), Encoding.UTF8, "application/json")).Result;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                _ = response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
