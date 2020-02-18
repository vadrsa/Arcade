using Infrastructure.Constants;
using Infrastructure.Prism;
using Infrastructure.Security;
using Kernel;
using Prism.Ioc;
using SharedEntities;
using StaffModule.Views;
using StaffModule.Workitems.SessionManager;
using StaffModule.Workitems.StaffManager;
using System.Threading.Tasks;

namespace Modules
{
    public class StaffModule : Module
    {

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);

            new Arcade.CustomControls.FaultedPage();
        }


    }
}
