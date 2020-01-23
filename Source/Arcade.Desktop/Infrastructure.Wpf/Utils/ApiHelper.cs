using Infrastructure.Api;
using Kernel.Managers;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public static class ApiHelper
    {
        static IUIManager UIManager;

        static ApiHelper()
        {
            UIManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>();
        }

        public static void HandleApiException(Exception exception, string defaultMessage = null, Action afterHandle = null)
        {
            if (exception is TaskCanceledException || exception.InnerException is TaskCanceledException)
                return;
            else
            {
                if (exception is ApiException)
                    UIManager.Error(exception.Message);
                else if (exception is ApiConnectionException)
                    UIManager.Error("Couldn't connect to server");
                else if (String.IsNullOrEmpty(defaultMessage))
                    UIManager.Error("An unkown error occured, please contact your administrator.");
                else
                    UIManager.Error(defaultMessage);
                afterHandle?.Invoke();
            }

        }
    }
}
