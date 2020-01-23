using DevExpress.Xpf.Bars;
using Prism;
using System;

namespace Infrastructure.Resources
{
    public class BarLinkHolder : BarItemLinkHolderBase, IActiveAware
    {

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    HandleActiveChanged();
                }
            }
        }

        public event EventHandler IsActiveChanged;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            HandleActiveChanged();
        }

        private void HandleActiveChanged()
        {
            bool visible = IsActive;
            foreach (var item in Items)
            {
                if (item is BarButtonItem)
                {
                    (item as BarButtonItem).IsVisible = visible;
                }
            }

            IsActiveChanged?.Invoke(this, new EventArgs());
        }
    }
}
