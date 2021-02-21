using CustomerNotification.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// File based logging 
        /// </summary>
        /// <param name="loggingViewModel"></param>
        public static void PostLogMessage(LoggingViewModel loggingViewModel)
        {
            FileLogger(JsonConvert.SerializeObject(loggingViewModel));
        }


        /// <summary>
        /// Logs to file
        /// </summary>
        /// <param name="lines"></param>
        public static void FileLogger(string lines)
        {
            string path = "Log/";
            VerifyDir(path);
            string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                file.Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Creates the directory
        /// </summary>
        /// <param name="path"></param>
        public static void VerifyDir(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { }
        }
    }
}
