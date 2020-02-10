using Infrastructure.Constants;
using Infrastructure.Workitems;
using Kernel;
using Kernel.Workitems;
using Prism.Ioc;
using SecurityModule.Features.Login;
using SecurityModule.Workitems.Login.Views;
using System.Windows;

namespace SecurityModule.Workitems.Login
{
    internal class LoginWorkitem : WorkitemWpfBase, ISupportsInitialization
    {
        IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public LoginWorkitem(IContainerExtension container) : base(container)
        {
        }

        public override void Configure()
        {
            base.Configure();
            Configuration.Configure(new ModalOptions(new Size(300, 300), ResizeMode.NoResize, WindowStartupLocation.CenterOwner, false, true));
        }

        public override string WorkItemName => "Login";

        protected override void RegisterViews(IViewContainer container)
        {
            base.RegisterViews(container);
            container.Register(new LoginView(), KnownRegions.Content);
        }

        public void Initialize(object data)
        {
            if(data is LoginWorkitemInitializationData)
                _authenticationService = ((LoginWorkitemInitializationData)data).AuthenticationService;
        }
    }
}
