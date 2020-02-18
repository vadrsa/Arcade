using Kernel.Configuration;
using System;
using System.Threading.Tasks;

namespace Kernel.Workitems
{
    public interface IWorkItem : ISupportsFocus, IDisposable
    {
        event EventHandler<EventArgs> IsDirtyChanged;

        /// <summary>
        /// Unique ID of the workitem
        /// </summary>
        string WorkItemID { get; }


        /// <summary>
        /// The Window workitem is displayed in
        /// Window is set only on workitems opened in modal mode
        /// </summary>
        IModalWindow Window { get; set; }

        /// <summary>
        /// The name of the Workitem, for example Product Manager
        /// </summary>
        string WorkItemName { get; }

        /// <summary>
        /// Shows if workitme has unsaved changes.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Specifies if the workitem is opened
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>
        /// Is the workitem opened in modal mode
        /// </summary>
        bool IsModal { get; }

        /// <summary>
        /// If True the workitem will remian focused after a child workitem is opened
        /// </summary>
        bool SupportsMultiFocus { get; }

        /// <summary>
        /// Provides configuration object for the workitem
        /// </summary>
        IOptionConfiguration Configuration { get; }

        /// <summary>
        /// Workitems Parent in the Workitem Tree
        /// </summary>
        IWorkItem Parent { get; set; }

        /// <summary>
        /// Close the workitem
        /// </summary>
        /// <returns></returns>
        Task<bool> Close();

        /// <summary>
        /// Configures the workitem
        /// Should be called before running the workitem
        /// </summary>
        void Configure();

        /// <summary>
        /// Launch the workitem
        /// </summary>
        Task<IObservable<WorkitemEventArgs>> Run();

        /// <summary>
        /// Launch the workitem in modal mode
        /// </summary>
        Task<IObservable<WorkitemEventArgs>> RunModal();

        void ChangeToModalState();

        /// <summary>
        /// Get a resource if it exists
        /// </summary>
        /// <param name="name">The name of the resource</param>
        /// <returns>The resource if found null otherwise</returns>
        object RequestResource(string name);



    }
}
