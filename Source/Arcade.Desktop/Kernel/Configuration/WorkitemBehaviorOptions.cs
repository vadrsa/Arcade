using Kernel.Workitems.Behaviors;

namespace Kernel.Configuration
{
    public class WorkitemBehaviorOptions
    {
        private WorkitemBehaviourCollection _behaviour = new WorkitemBehaviourCollection();

        public IWorkitemBehaviour Behaviour => _behaviour;

        public WorkitemBehaviorOptions Attach(IWorkitemBehaviour behaviour)
        {
            _behaviour.Register(behaviour);
            return this;
        }
    }
}
