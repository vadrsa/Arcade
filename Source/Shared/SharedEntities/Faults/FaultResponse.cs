namespace SharedEntities
{
    public class FaultResponse
    {
        public int Status { get; set; }

        public string TraceId { get; set; }

        public string Message { get; set; }

        public int FaultCode { get; set; }

        public FaultResponse(int code, string message, string traceId, int statusCode)
        {
            FaultCode = code;
            Message = message;
            TraceId = traceId;
            Status = statusCode;
        }

    }

    
}
