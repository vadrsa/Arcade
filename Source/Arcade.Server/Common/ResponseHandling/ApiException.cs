using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ResponseHandling
{
    public class ApiException : Exception
    {
        public FaultCode FaultCode { get; private set; }
        public string OverrideMessage { get; private set; }

        public ApiException(FaultCode faultCode)
        {
            this.FaultCode = faultCode;
        }

        public ApiException(FaultCode faultCode, string message)
            : this(faultCode)
        {
            this.OverrideMessage = message;
        }
    }
}
