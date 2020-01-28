using Kernel.Utility;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Kernel.Workitems.Strategies.Launch
{
    /// <summary>
    /// Launch root workitem strategy
    /// </summary>
    internal class RootWorkitemLaunchStrategy : WorkitemLaunchStrategy
    {
        public RootWorkitemLaunchStrategy(ContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null) : base(currentContextService, workItem, parent, data)
        {
        }

        /// <summary>
        /// Launch the root workitem
        /// </summary>
        /// <returns></returns>
        protected override async Task Execute()
        {
            // get the workitem type
            Type type = Workitem.GetType();
            // if should open modal set the window owner to MainWindow
            if (ShouldOpenModal)
            {
                if(Workitem.Window is Window)
                {
                    Application.Current.Dispatcher.InvokeIfNeeded(() => ((Window)Workitem.Window).Owner = Application.Current.MainWindow);
                }
            }

            await RunWorkitem().ConfigureAwait(false);



        }
    }
}
