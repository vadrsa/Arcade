using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ResponseHandling
{
    public abstract class ApiResponse
    {
        public string Message { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(string message)
        {
            Message = message;
        }
    }
}
