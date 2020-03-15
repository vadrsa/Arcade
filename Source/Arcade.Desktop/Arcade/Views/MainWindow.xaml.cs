using Infrastructure.Mvvm;
using Kernel.Managers;
using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Arcade.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            snackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(3000));
            //snackbar.MessageQueue.Enqueue("test");
            MessageQueueContainer.Queue = snackbar.MessageQueue;
            MessageQueueContainer.Queue.IgnoreDuplicate = true;
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = !CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>()?.AskForConfirmation("Are you sure you want to exit? Unsaved changes will be lost.") ?? false;
        }
    }
}
