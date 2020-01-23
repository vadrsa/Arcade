using Flurl.Http;
using Infrastructure.Security;
using System;
using System.Configuration;
using System.IO;

namespace Infrastructure.Api
{
    public abstract class RestConsumingServiceBase
    {
        private string _baseUrl;

        private string _controllerName;

        public RestConsumingServiceBase(string controllerName, string url)
        {
            _controllerName = controllerName;
            _baseUrl = url;
        }

        protected string ControllerName
        {
            get
            {
                return _controllerName;
            }
        }

        protected string ControllerPath
        {
            get
            {
                return _baseUrl + ControllerName + "/";
            }
        }

        protected IFlurlRequest BuildRequest(string url = "")
        {
            IFlurlRequest request = Path.Combine(ControllerPath, url).ConfigureRequest(_ => { });
            if (AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
                request = request.WithHeader("Authorization", String.Format("Bearer {0}", AppSecurityContext.CurrentPrincipal.Identity.Token));
            return request;
        }
    }
}
