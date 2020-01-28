using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kernel.Workitems.Behaviors
{
    public class WorkitemBehaviourCollection : IWorkitemBehaviour
    {

        private List<IWorkitemBehaviour> behaviours = new List<IWorkitemBehaviour>();

        public async Task<bool> OnLaunching(IContextService service, WorkitemLaunchDescriptor launchDescriptor)
        {
            bool result = true;
            foreach (var behaviour in behaviours)
            {
                try
                {
                    if (!await behaviour.OnLaunching(service, launchDescriptor))
                        result = false;
                }
                catch (Exception e)
                {

                }
            }
            return result;
        }

        public void Register(IWorkitemBehaviour workitemBehavior)
        {
            behaviours.Add(workitemBehavior);
        }
    }
}
