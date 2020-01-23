using Common.Enums;
using Common.ResponseHandling;
using System.Collections.Generic;
using Common.Core;
using System.Net;

namespace Common.Configuration
{
    public class ResponseOptions
    {
        public LocalizationDictionary<object, ApiResponse> Dictionary { get; set; }
        public ResponseOptions()
        {
        }
    }
}
