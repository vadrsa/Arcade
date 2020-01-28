using Kernel.Workitems;
using Kernel.Workitems.Behaviors;
using System.Linq;
using System.Threading.Tasks;

namespace Arcade.Behaviours
{
    class WorkitemOneOfTypeLaunchBehaviour : IWorkitemBehaviour
    {
        public async Task<bool> OnLaunching(IContextService service, WorkitemLaunchDescriptor launchDescriptor)
        {
            var ofType = service.Workitems.FirstOrDefault(w => w.GetType().Equals(launchDescriptor.Type));
            if (ofType != null)
            {
                await service.FocusWorkitem(ofType);
                return false;
            }
            else
            {
                if (launchDescriptor.IsRoot &&
                    !launchDescriptor.IsModal &&
                    service.Workitems.Where(w => w.Parent == null && !w.IsModal).Count() == 1)
                {
                    await service.CloseWorkitem(service.Workitems.First(w => w.Parent == null && !w.IsModal));
                }
            }
            return true;
        }
    }
}
