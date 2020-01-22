using Flurl.Http;
using PostSharp.Aspects;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Api
{

    [Serializable]
    public class ApiExceptionHandlingAttribute : MethodInterceptionAspect
    {
        #region Fields


        #endregion

        public ApiExceptionHandlingAttribute() { }

        #region Public Properties

        #endregion

        #region Public Methods and Operators

        public async override Task OnInvokeAsync(MethodInterceptionArgs args)
        {
            try
            {
                await args.ProceedAsync();
            }
            catch (FlurlHttpException ex)
            {
                await HandleHttpError(ex).ConfigureAwait(false);
            }
        }
        public async override void OnInvoke(MethodInterceptionArgs args)
        {
            try
            {
                args.Proceed();
            }
            catch (FlurlHttpException ex)
            {
                await HandleHttpError(ex).ConfigureAwait(false);
            }
        }

        private async Task HandleHttpError(FlurlHttpException ex)
        {
            if (ex?.InnerException is TaskCanceledException)
                throw ex.InnerException;
            if (ex.Call.Response == null)
            {
                throw new ApiConnectionException("Couldn't connect to server");
            }
            else if (ex.Call.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized || ex.Call.Response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new ApiException("You are unauthorized to access the resource.", ex.Call.Response.StatusCode);
            }
            else
            {
                ApiError error = await ex.GetResponseJsonAsync<ApiError>().ConfigureAwait(false);
                throw new ApiException(error.Message, error.Status);
            }
        }

        #endregion
    }
}
