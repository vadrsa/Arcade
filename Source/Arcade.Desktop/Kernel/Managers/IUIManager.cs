using System.Windows;

namespace Kernel.Managers
{
    public interface IUIManager
    {
        MessageBoxResult ShowMessageBox(string message, string caption, System.Windows.MessageBoxButton buttons = System.Windows.MessageBoxButton.OK);
        void Error(string message);
        bool AskForConfirmation(string message);
    }
}
