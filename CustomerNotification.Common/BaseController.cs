using CustomerNotification.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CustomerNotification.Common
{
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Correlation id of the request
        /// </summary>
        protected Guid _corelationId = Guid.Empty;
        /// <summary>
        /// Configuration
        /// </summary>
        protected IConfiguration _configuration;
        private bool _isDevMode = false;

        /// <summary>
        /// Constructor for Base controller
        /// </summary>
        /// <param name="configuration"></param>
        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            bool.TryParse(_configuration["IsDevMode"], out _isDevMode);
        }

        protected bool IsDevMode
        {
            get { return _isDevMode; }
        }

        /// <summary>
        /// Reads the correlationId from the header
        /// </summary>
        protected void InitializeCorelation()
        {
            if (Request != null)
            {
                Guid.TryParse(Request.Headers["correlationId"], out _corelationId);

                if (_corelationId == Guid.Empty)
                {
                    _corelationId = Guid.NewGuid();
                    Request.Headers["correlationId"] = new StringValues(_corelationId.ToString());
                }

            }
        }

        #region HTTP Status Code
        /// <summary>
        /// Returns 500InternalServerError status result
        /// </summary>		
        protected StatusCodeResult InternalServerError()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        /// <summary>
        /// Returns 500InternalServerError with ObjectResult
        /// </summary>
        /// <param name="value">object</param>		
        protected ObjectResult InternalServerError(object value)
        {
            return new ObjectResult(value) { StatusCode = StatusCodes.Status500InternalServerError };
        }



        #endregion


        #region Logging Methods
        /// <summary>
        /// Log Information
        /// </summary>
        /// <param name="methodName">Name of the method where the context belongs to</param>
        /// <param name="message">Message to be logged</param>
        protected void LogInfo(string methodName, string message)
        {
            var loggerModel = new LoggingViewModel();

            loggerModel.CorrelationId = _corelationId;
            loggerModel.ApplicationName = this.GetType().Namespace;
            loggerModel.ClassName = this.GetType().Name;
            loggerModel.MethodName = methodName;
            loggerModel.LogType = LogTypeClassificationEnum.INFO;
            loggerModel.ErrorMessage = message;
            loggerModel.Request = null;
            loggerModel.Response = null;
            loggerModel.Ex = null;

            BaseService.PostLogMessage(loggerModel, _configuration["LoggingSection:LoggingServiceURL"]);

            //Logger.Log(_corelationId, this.GetType().Namespace,
            //    this.GetType().Name, methodName,
            //    LogTypeEnum.INFO,
            //    message, null, null, null, _loggedInUserID);

        }

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="methodName">Name of the method where the context belongs to</param>
        /// <param name="message">Message to be logged</param>
        /// <param name="ex">Exception object</param>
        protected void LogError(string methodName, string message, Exception ex)
        {
            var loggerModel = new LoggingViewModel();

            loggerModel.CorrelationId = _corelationId;
            loggerModel.ApplicationName = this.GetType().Namespace;
            loggerModel.ClassName = this.GetType().Name;
            loggerModel.MethodName = methodName;
            loggerModel.LogType = LogTypeClassificationEnum.ERROR;
            loggerModel.ErrorMessage = message;
            loggerModel.Request = null;
            loggerModel.Response = null;
            loggerModel.Ex = ex;

            BaseService.PostLogMessage(loggerModel, _configuration["LoggingSection:LoggingServiceURL"]);
            //Logger.Log(_corelationId, this.GetType().Namespace,
            //    this.GetType().Name, methodName,
            //    LogTypeEnum.ERROR,
            //    message, null, null, ex, _loggedInUserID);
        }

        /// <summary>
        /// Log Warning
        /// </summary>
        /// <param name="methodName">Name of the method where the context belongs to</param>
        /// <param name="message">Message to be logged</param>
        protected void LogWarning(string methodName, string message)
        {
            var loggerModel = new LoggingViewModel();

            loggerModel.CorrelationId = _corelationId;
            loggerModel.ApplicationName = this.GetType().Namespace;
            loggerModel.ClassName = this.GetType().Name;
            loggerModel.MethodName = methodName;
            loggerModel.LogType = LogTypeClassificationEnum.WARNING;
            loggerModel.ErrorMessage = message;
            loggerModel.Request = null;
            loggerModel.Response = null;
            loggerModel.Ex = null;

            BaseService.PostLogMessage(loggerModel, _configuration["LoggingSection:LoggingServiceURL"]);

        }

        protected ObjectResult HandleException(Exception ex)
        {
            var msg = "Unhandled Exception occured.";

            var callingMethod = new StackFrame(1).GetMethod().Name;

            LogError(callingMethod, $"{msg} {ex.Message}", ex);

            if (IsDevMode)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        #endregion
    }
}
