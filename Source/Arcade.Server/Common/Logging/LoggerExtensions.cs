using Common.Enums;
using Common.ResponseHandling;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Logging
{
    public static class LoggerExtensions
    {
        public static void LogFault(this ILogger logger, ApiException exception)
        {
            logger.LogError(exception, String.Format("Fault: '{0}'|Fault Code: {1}|", exception.FaultCode.ToString(), (int)exception.FaultCode));
        }
    }
}
