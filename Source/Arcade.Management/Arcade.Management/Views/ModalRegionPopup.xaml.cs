using DevExpress.Xpf.Core;
using Kernel;
using Kernel.Configuration;
using Kernel.Managers;
using Kernel.Prism;
using Kernel.Utility;
using Kernel.Workitems;
using System.Windows;

namespace Arcade.Management.Views
{
    /// <summary>
    /// Interaction logic for ModalRegionPopup.xaml
    /// </summary>
    public partial class ModalRegionPopup : ThemedWindow, IModalWindow
    {
        ITaskManager TaskManager => CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();

        public ModalRegionPopup(Project project)
        {
            var names = new DynamicRegionNames(project);
            DynamicRegionManager.SetDynamicRegionNames(this, names);
            InitializeComponent();
        }

        #region IWindow Implementation

        void IModalWindow.Focus()
        {

            TaskManager.RunUIThread(() =>
            {
                UIHelper.TryFocusWindow(this);
            });
        }

        void IModalWindow.Unfocus()
        {

            TaskManager.RunUIThread(() =>
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            });
        }

        public DependencyObject GetRegionHolder()
        {
            return this;
        }
        #endregion
    }
}
