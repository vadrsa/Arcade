using DevExpress.Xpf.Core;
using Kernel;
using Kernel.Prism;
using Kernel.Workitems;
using System.Windows;

namespace Infrastructure.Prism
{
    public class DXTabControlRegionTransformation : RegionTransformationBase<DXTabControl>
    {
        public override object Transform(object view)
        {
            DXTabItem item = null;
            IWorkItem workitem = null;
            string header = "Tab";

            if (view is DXTabItem)
                item = (DXTabItem)view;

            if (view is DependencyObject)
                workitem = WorkitemManager.GetOwner((DependencyObject)view);

            if (workitem != null)
                header = workitem.WorkItemName;

            item = new DXTabItem() { Content = view, Header = header };

            if (workitem != null)
            {
                SetTabHeader(item, workitem.WorkItemName, workitem.IsDirty);

                WorkitemManager.SetOwner(item, workitem);
                // TODO: unregister on workitem close
                workitem.IsDirtyChanged += (o, e) =>
                {
                    SetTabHeader(item, workitem.WorkItemName, workitem.IsDirty);
                };
            }
            return item;
        }

        private static void SetTabHeader(DXTabItem item, string header, bool isDirty)
        {
            if (isDirty)
                item.Header = header + "*";
            else
                item.Header = header;
        }
    }
}
