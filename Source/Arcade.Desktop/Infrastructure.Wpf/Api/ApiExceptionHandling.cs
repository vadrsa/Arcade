using Flurl.Http;
using SharedEntities;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Infrastructure.Api
{
    public static class ApiExceptionHandling
    {
        public static string GetStatusCodeMessage(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return "Resource access unauthorized";
                default:
                    return null;
            }
        }

        public static async Task<FaultResponse> GetResponse(Exception e)
        {
            if (e is FlurlHttpException)
            {
                var flurlEx = (FlurlHttpException)e;
                FaultResponse response;
                try
                {
                    response = await flurlEx.GetResponseJsonAsync<FaultResponse>();
                    if(response != null)
                        return response;
                }
                catch
                {
                    return new UnexpectedFault();
                }

                if (!flurlEx.Call.Completed)
                    return new ConnectionFailedFault();
                else
                {
                    string message = GetStatusCodeMessage(flurlEx.Call.Response.StatusCode);
                    if (message == null)
                        return new UnexpectedFault();
                    else
                        return new FaultResponse(0, message, null, (int)flurlEx.Call.Response.StatusCode);
                }
            }
            else
                return new UnexpectedFault();
        }
    }
}
