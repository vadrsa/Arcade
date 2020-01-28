using System.Threading.Tasks;

namespace Kernel.Workitems.Behaviors
{
    public interface IWorkitemBehaviour
    {
        Task<bool> OnLaunching(IContextService service, WorkitemLaunchDescriptor launchDescriptor);
    }
}
