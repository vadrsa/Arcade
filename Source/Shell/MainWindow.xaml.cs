using DevExpress.Xpf.Core;
using Kernel.Managers;
using System.ComponentModel;

namespace Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = !CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>()?.AskForConfirmation("Are you sure you want to exit? Unsaved changes will be lost.") ?? false;
        }
    }
}
