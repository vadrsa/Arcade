using System.Threading.Tasks;

namespace Kernel.Workitems.Strategies.Close
{
    /// <summary>
    /// Close startegy for root workitems
    /// </summary>
    internal class RootWorkitemCloseStrategy : WorkitemCloseStrategy
    {
        public RootWorkitemCloseStrategy(ContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override async Task Execute()
        {
            // if not modal
            if (!Workitem.IsModal)
            {
                // focus next workitem
                IWorkItem toFocus = null;

                if (Workitem.IsFocused && CurrentContextService.Collection.Count > 1)
                {
                    int index = CurrentContextService.Collection.IndexOf(Workitem);
                    if (index > 0)
                        toFocus = CurrentContextService.Collection[index - 1];
                    else if (index == 0)
                        toFocus = CurrentContextService.Collection[index + 1];
                }
                else if (Workitem.IsFocused && CurrentContextService.Collection.Count <= 1)
                    await CurrentContextService.FocusWorkitem(null);

                if (toFocus != null)
                    await CurrentContextService.FocusWorkitem(toFocus);
            }

        }
    }
}
