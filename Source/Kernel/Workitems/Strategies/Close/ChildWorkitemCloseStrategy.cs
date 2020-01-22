using System.Threading.Tasks;

namespace Kernel.Workitems.Strategies.Close
{
    /// <summary>
    /// Close startegy for child workitems
    /// </summary>
    internal class ChildWorkitemCloseStrategy : WorkitemCloseStrategy
    {
        internal ChildWorkitemCloseStrategy(ContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override async Task Execute()
        {
            // if not modal
            if (!(Workitem.IsModal))
            {
                await CurrentContextService.FocusWorkitem((IWorkItem)Workitem.Parent);
            }

        }
    }
}
