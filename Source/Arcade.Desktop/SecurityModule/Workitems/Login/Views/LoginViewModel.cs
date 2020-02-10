using Flurl.Http;
using Infrastructure.Api;
using Infrastructure.Mvvm;
using Infrastructure.Security;
using SharedEntities;
using SharedEntities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SecurityModule.Workitems.Login.Views
{
    class LoginViewModel : WorkitemViewModel<LoginWorkitem>
    {

        public LoginViewModel()
        {
        }

        private string errorText;

        public string ErrorText
        {
            get { return errorText; }
            set { Set(ref errorText, value, nameof(ErrorText)); }
        }

        private string username = "vadrsa";

        public string Username
        {
            get { return username; }
            set { Set(ref username, value, nameof(Username)); }
        }

        public PasswordBox PasswordBox
        {
            get;
            set;
        }

        private AsyncCommand loginCommand;
        public AsyncCommand LoginCommand =>
            loginCommand ?? (loginCommand = new AsyncCommand(ExecuteLoginCommand));


        async Task ExecuteLoginCommand()
        {
            //IsLoginCommandExecuting = true;
            //LoginCommand?.RaiseCanExecuteChanged();
            //try
            //{
            //    cancellationToken?.Cancel();
            //    cancellationToken = new CancellationTokenSource();
            await LoadCustom(DoExecuteLoginCommand);
            //}
            //catch (FlurlHttpException ex)
            //{
            //    if(ex.Call.Response == null)
            //    {
            //        ErrorText = "Couldn't connect to server";
            //    }
            //    else
            //    {
            //        FaultResponse response = await ex.GetResponseJsonAsync<FaultResponse>();
            //        if (response == null)
            //            ErrorText = "An unkown error occured, please contact your administrator.";
            //        else
            //            ErrorText = response.Message;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrorText = "An unkown error occured, please contact your administrator.";
            //}
            //finally
            //{
            //    IsLoginCommandExecuting = false;
            //    LoginCommand?.RaiseCanExecuteChanged();

            //}
        }

        //protected override string GetFaultMessage(FaultResponse response)
        //{
        //    if(response.Descriptor != null && response.Descriptor is IEnumerable<string>)
        //        return ((IEnumerable<string>)response.Descriptor).Aggregate("", (str, err) => str += err + '\n');

        //    return null;
        //}

        private async Task DoExecuteLoginCommand(CancellationToken token)
        {
            await ((LoginWorkitem)Workitem).AuthenticationService.AuthenticateAsync(Username, PasswordBox?.Password, token);
            await Workitem.Close();
        }

    }
}
