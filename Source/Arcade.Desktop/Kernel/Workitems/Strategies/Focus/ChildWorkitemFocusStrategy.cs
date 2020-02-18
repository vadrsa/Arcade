using System.Linq;
using System.Threading.Tasks;

namespace Kernel.Workitems.Strategies.Focus
{
    internal class ChildWorkitemFocusStrategy : WorkitemFocusStrategy
    {
        public ChildWorkitemFocusStrategy(ContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override async Task Execute()
        {
            Workitem.IsFocused = true;
            if (CurrentContextService.Collection.Null != null)
                CurrentContextService.Collection.Null.IsFocused = false;
            foreach (IWorkItem item in CurrentContextService.Collection.ToList())
            {
                if (item.Equals(Workitem.Parent) && Workitem.Parent.SupportsMultiFocus)
                    continue;
                if (!item.Equals(Workitem))
                    item.IsFocused = false;
            }

        }
    }
}
