using System.Net;

namespace Infrastructure.Api
{
    public class ApiError
    {
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
        public string TraceID { get; set; }

    }
}
