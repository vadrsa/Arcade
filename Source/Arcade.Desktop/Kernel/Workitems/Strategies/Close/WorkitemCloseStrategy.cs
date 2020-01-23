using Kernel.Managers;
using Kernel.Utility;
using System;
using System.Threading.Tasks;

namespace Kernel.Workitems.Strategies.Close
{

    /// <summary>
    /// The close strategy for a workitem
    /// Any workitem should be closed by calling 
    /// WorkitemCloseStrategy.Close method
    /// </summary>
    internal abstract class WorkitemCloseStrategy
    {

        /// <summary>
        /// Applciation task manager
        /// </summary>
        protected ITaskManager TaskManager { get; private set; }

        /// <summary>
        /// Current ContextService
        /// </summary>
        protected ContextService CurrentContextService { get; private set; }

        /// <summary>
        /// Workitem to close
        /// </summary>
        protected IWorkItem Workitem { get; private set; }

        internal WorkitemCloseStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            CurrentContextService = currentContextService;
            Workitem = workItem;
            TaskManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();
        }

        /// <summary>
        /// Get the startegy to close the workitem
        /// The close startegy should be obtained from 
        /// here and never be instaciated outside of this method
        /// </summary>
        /// <param name="currentContextService">context service</param>
        /// <param name="workItem">workitem to close</param>
        /// <returns></returns>
        public static WorkitemCloseStrategy GetCloseStrategy(ContextService currentContextService, IWorkItem workItem)
        {
            // if wokritem has parent use ChildWorkitemCloseStrategy otherwise RootWorkitemCloseStrategy
            if (workItem.Parent != null)
                return new ChildWorkitemCloseStrategy(currentContextService, workItem);
            else
                return new RootWorkitemCloseStrategy(currentContextService, workItem);
        }

        /// <summary>
        /// Instance specific close logic
        /// </summary>
        protected abstract Task Execute();

        /// <summary>
        /// Close the workitem
        /// </summary>
        public async Task Close()
        {
            // NullWorkitem cannot be closed
            if (Workitem is NullWorkitem)
                return;

            //Logger.LogWithWorkitemData("Closing workitem", LogLevel.Informative, Workitem);

            // if the workitem is not open fix collection and return
            if (!Workitem.IsOpen)
            {
                //Logger.Log("Workitem is already closed, trying to remove referances", LogLevel.Informative);
                if (CurrentContextService.Collection.Contains(Workitem))
                    CurrentContextService.Collection.Remove(Workitem);
                return;
            }

            // set isOpen
            Workitem.IsOpen = false;

            try
            {
                // execute instance specific close logic
                await Execute().ConfigureAwait(false);

                // dispose of the workitem
                Workitem.Dispose();

                // clear commands
                CommandManager.ClearWorkitemCommands(Workitem);

                // remove views in UI thread
                TaskManager.RunUIThread(() => CurrentContextService.RegionManager.RemoveWorkitemViews(Workitem));

                // if modal workitem close window
                if (Workitem.IsModal)
                    TaskManager.RunUIThread(() => Workitem.Window.TryClose());

                // remove from the collection
                CurrentContextService.Collection.Remove(Workitem);
            }
            catch (Exception e)
            {
                // Log workitem exception
                //Logger.LogWithWorkitemData("Failed to close workitem", LogLevel.Exception, Workitem, e);
            }
            finally
            {
                // collect garbage
                GC.Collect();
            }
            //Logger.LogWithWorkitemData("Workitem closed", LogLevel.Informative, Workitem);
        }
    }
}
