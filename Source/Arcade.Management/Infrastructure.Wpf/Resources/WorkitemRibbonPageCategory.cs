using DevExpress.Xpf.Ribbon;
using Prism;
using System;

namespace Infrastructure.Resources
{
    public class WorkitemRibbonPageCategory : RibbonPageCategory, IActiveAware
    {

        public WorkitemRibbonPageCategory()
        {
            base.IsVisible = false;
        }

        public event EventHandler<EventArgs> VisibilityChanged;

        private bool isVisble;
        public new bool IsVisible
        {
            get { return isVisble; }
            set
            {
                if (value != isVisble)
                {
                    base.IsVisible = value;
                    isVisble = value;
                    VisibilityChanged?.Invoke(this, new EventArgs());
                }
            }
        }

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
            if (IsActive)
                this.IsVisible = true;
            else
                this.IsVisible = false;
            IsActiveChanged?.Invoke(this, new EventArgs());
        }
    }
}
