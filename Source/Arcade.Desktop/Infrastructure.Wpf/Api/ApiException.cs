using System;
using System.Net;

namespace Infrastructure.Api
{
    public class ApiException : Exception
    {

        public HttpStatusCode HttpStatusCode { get; set; }
        public ApiException() { }
        public ApiException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
