using Infrastructure.Mvvm;
using Kernel.Managers;
using MaterialDesignMessageBox;
using System.Windows;

namespace Infrastructure.Prism
{
    public class UIManager : IUIManager
    {


        public MessageBoxResult ShowMessageBox(string message, string caption, System.Windows.MessageBoxButton buttons = System.Windows.MessageBoxButton.OK)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                return Application.Current.Dispatcher.Invoke(() =>
                {
                    return MaterialMessageBox.ShowDialog(Application.Current.MainWindow, message, caption, buttons, stylePrimaryColor: MaterialDesignColors.PrimaryColor.DeepPurple);
                });
            }
            else
            {
                return MaterialMessageBox.ShowDialog(Application.Current.MainWindow, message, caption, buttons, stylePrimaryColor: MaterialDesignColors.PrimaryColor.DeepPurple);
            }
        }

        public void Error(string message)
        {
            MessageQueueContainer.Queue.Enqueue(message);
        }

        public bool AskForConfirmation(string message)
        {
            return ShowMessageBox(message, "Confirmation Needed", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes;
        }
    }

}
