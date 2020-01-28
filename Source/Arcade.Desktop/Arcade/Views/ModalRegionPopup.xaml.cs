using Kernel;
using Kernel.Configuration;
using Kernel.Managers;
using Kernel.Prism;
using System.Windows.Controls;

namespace Arcade.Views
{
    /// <summary>
    /// Interaction logic for ModalRegionPopup.xaml
    /// </summary>
    public partial class ModalRegionPopup : UserControl
    {
        ITaskManager TaskManager => CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();

        public ModalRegionPopup(Project project)
        {
            var names = new DynamicRegionNames(project);
            DynamicRegionManager.SetDynamicRegionNames(this, names);
            InitializeComponent();
        }

    }
}
