using System.Threading.Tasks;

namespace Kernel.Workitems.Strategies.Focus
{
    internal class ModalWorkitemFocusStrategy : WorkitemFocusStrategy
    {
        public ModalWorkitemFocusStrategy(ContextService currentContextService, IWorkItem workItem) : base(currentContextService, workItem)
        {
        }

        protected override async Task Execute()
        {
            await TaskManager.Run(() =>
            {
                Workitem.IsFocused = true;
            }).ConfigureAwait(false);
        }
    }
}
