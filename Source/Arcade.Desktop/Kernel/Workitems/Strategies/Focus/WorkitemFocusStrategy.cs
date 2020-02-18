using Kernel.Managers;
using System;
using System.Threading.Tasks;

namespace Kernel.Workitems.Strategies.Focus
{
    /// <summary>
    /// The focus strategy for a workitem
    /// Any workitem should be focused by calling 
    /// WorkitemFocusStrategy.Focus method
    /// </summary>
    internal abstract class WorkitemFocusStrategy
    {
        /// <summary>
        /// The Current ContextService
        /// </summary>
        protected ContextService CurrentContextService { get; private set; }

        /// <summary>
        /// The workitem to focus
        /// </summary>
        protected IWorkItem Workitem { get; private set; }

        /// <summary>
        /// Applciation task manager
        /// </summary>
        protected ITaskManager TaskManager { get; private set; }

        internal WorkitemFocusStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            CurrentContextService = currentContextService;
            Workitem = workItem;
            TaskManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();
        }

        /// <summary>
        /// Get the startegy to focus the workitem
        /// The focus startegy should be obtained from 
        /// here and never be instaciated outside of this method
        /// </summary>
        /// <param name="contextService">ContextService</param>
        /// <param name="workItem">Workitem to open</param>
        public static WorkitemFocusStrategy GetFocusStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            if (workItem == null || workItem is NullWorkitem)
                return new WorkitemUnfocusStrategy(currentContextService, workItem);
            else if (workItem.IsModal)
                return new ModalWorkitemFocusStrategy(currentContextService, workItem);
            else if (workItem.Parent != null)
                return new ChildWorkitemFocusStrategy(currentContextService, workItem);
            else
                return new RootWorkitemFocusStrategy(currentContextService, workItem);
        }

        /// <summary>
        /// Focus the workitem
        /// </summary>
        public async Task Focus()
        {
            try
            {
                await Execute().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                // Log workitem exception
                //Logger.LogWithWorkitemData("Failed to focus workitem", LogLevel.Exception, Workitem, e);
            }
        }

        protected abstract Task Execute();
    }
}
