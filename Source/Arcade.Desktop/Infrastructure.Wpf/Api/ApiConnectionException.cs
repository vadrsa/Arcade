using System;

namespace Infrastructure.Api
{
    public class ApiConnectionException : Exception
    {

        public ApiConnectionException() { }
        public ApiConnectionException(string message) : base(message) { }
    }
}
