using Kernel.Managers;
using System;
using System.Windows;

namespace Arcade.Services
{
    public class UIManager : IUIManager
    {
        public bool AskForConfirmation(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public MessageBoxResult ShowMessageBox(string message, string caption, MessageBoxButton buttons = MessageBoxButton.OK)
        {
            return MessageBox.Show(message);
        }
    }
}
