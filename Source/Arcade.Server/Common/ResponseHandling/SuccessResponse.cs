using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ResponseHandling
{
    public class SuccessResponse : ApiResponse
    {
        public SuccessResponse(): base()
        {

        }

        public SuccessResponse(string message) : base(message)
        {
        }
    }
}
