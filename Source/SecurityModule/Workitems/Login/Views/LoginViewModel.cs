using DevExpress.Mvvm;
using Infrastructure.Api;
using Infrastructure.Mvvm;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityModule.Workitems.Login.Views
{
    public class LoginViewModel : WorkitemViewModel
    {
        CancellationTokenSource cancellationToken;


        public LoginViewModel()
        {
        }

        private string errorText;

        public string ErrorText
        {
            get { return errorText; }
            set { SetProperty(ref errorText, value, nameof(ErrorText)); }
        }


        private string username = "";

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value, nameof(Username)); }
        }


        private string password = "";

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value, nameof(Password)); }
        }

        private AsyncCommand loginCommand;
        public AsyncCommand LoginCommand =>
            loginCommand ?? (loginCommand = new AsyncCommand(ExecuteLoginCommand, CanExecuteLoginCommand));

        bool IsLoginCommandExecuting;

        async Task ExecuteLoginCommand()
        {
            IsLoginCommandExecuting = true;
            LoginCommand?.RaiseCanExecuteChanged();
            try
            {
                cancellationToken?.Cancel();
                cancellationToken = new CancellationTokenSource();
                await ((LoginWorkitem)Workitem).AuthenticationService.AuthenticateAsync(Username, Password, cancellationToken.Token);
                await Workitem.Close();
            }
            catch (ApiException apiEx)
            {
                //Logger.LogErrorSource("Error while logging in", apiEx);
                ErrorText = apiEx.Message;
            }

            catch (ApiConnectionException)
            {
                //Logger.LogErrorSource("Error while logging in | Couldn't connect to server");
                ErrorText = "Couldn't connect to server";
            }
            catch (Exception e)
            {
                //Logger.LogErrorSource("Unknown error while logging in", e);
                ErrorText = "An unkown error occured, please contact your administrator.";
            }
            finally
            {
                IsLoginCommandExecuting = false;
                LoginCommand?.RaiseCanExecuteChanged();

            }
        }

        bool CanExecuteLoginCommand()
        {
            return !IsLoginCommandExecuting;
        }

        public override void Dispose()
        {
            base.Dispose();
            cancellationToken?.Cancel();
        }
    }
}
