using System.Windows;

namespace Kernel.Managers
{
    internal class UIManager : IUIManager
    {
        public MessageBoxResult ShowMessageBox(string message, string caption, System.Windows.MessageBoxButton buttons = System.Windows.MessageBoxButton.OK)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                return Application.Current.Dispatcher.Invoke(() =>
                {
                    return MessageBox.Show(message, caption, buttons);

                });
            }
            else
            {
                return MessageBox.Show(message, caption, buttons);
            }
        }

        public void Error(string message)
        {
            ShowMessageBox(message, "Error", MessageBoxButton.OK);
        }

        public bool AskForConfirmation(string message)
        {
            return ShowMessageBox(message, "Confirmation Needed", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes;
        }
    }
}
