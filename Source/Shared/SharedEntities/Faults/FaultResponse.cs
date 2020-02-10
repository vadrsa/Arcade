namespace SharedEntities
{
    public class FaultResponse
    {
        public int Status { get; set; }

        public string TraceId { get; set; }

        public string Message { get; set; }

        public int FaultCode { get; set; }

        public object Descriptor { get; set; }

        public FaultResponse(int code, string message, string traceId, int statusCode, object descriptor = null)
        {
            FaultCode = code;
            Message = message;
            TraceId = traceId;
            Status = statusCode;
            Descriptor = descriptor;
        }

    }

    
}
