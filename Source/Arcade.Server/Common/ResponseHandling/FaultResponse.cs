using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.ResponseHandling
{
    public class FaultResponse : ApiResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        
        public FaultResponse(HttpStatusCode code) : base(null)
        {
            HttpStatusCode = code;
        }
        public FaultResponse(HttpStatusCode code, string message) : this(code)
        {
            Message = message;
        }

        public static FaultResponse Default
        {
            get
            {
                return new FaultResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
