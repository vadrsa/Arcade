using Common.Faults;
using Microsoft.Extensions.Logging;
using System;

namespace Common.Logging
{
    public static class LoggerExtensions
    {
        public static void LogFault(this ILogger logger, string traceIdentifier, FaultException exception)
        {
            logger.LogError(exception, String.Format("TraceId: {2}| Fault: '{0}'| Fault Type: {1}|", exception.Type.ToString(), (int)exception.Type, traceIdentifier));
        }
    }
}
