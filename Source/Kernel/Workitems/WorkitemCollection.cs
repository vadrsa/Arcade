using System.Collections.ObjectModel;

namespace Kernel.Workitems
{
    public class WorkitemCollection : ObservableCollection<IWorkItem>
    {
        public IWorkItem Get(string id)
        {
            foreach (var w in this)
            {
                if (w.WorkItemID == id)
                    return w;
            }
            return null;
        }

        IWorkItem nullWorkitem;
        public IWorkItem Null
        {
            get
            {
                return nullWorkitem;
            }
            set
            {
                nullWorkitem = value;
            }
        }
    }
}
