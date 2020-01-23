using System.Linq;
using System.Threading.Tasks;

namespace Kernel.Workitems.Strategies.Focus
{
    internal class RootWorkitemFocusStrategy : WorkitemFocusStrategy
    {
        public RootWorkitemFocusStrategy(ContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override async Task Execute()
        {
            Workitem.IsFocused = true;
            if (CurrentContextService.Collection.Null != null)
                CurrentContextService.Collection.Null.IsFocused = false;
            foreach (IWorkItem item in CurrentContextService.Collection.ToList())
            {
                if (!item.Equals(Workitem))
                    item.IsFocused = false;
            }

        }
    }
}
