using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerNotification.Common.Models
{
    public class LoggingViewModel
    {
        public Guid? CorrelationId { get; set; }
        public string ApplicationName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public LogTypeClassificationEnum LogType { get; set; }
        public string ErrorMessage { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public Exception Ex { get; set; }

    }

    public enum LogTypeClassificationEnum
    {
        DEBUG,
        INFO,
        WARNING,
        ERROR
    }
}
