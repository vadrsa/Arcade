using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace MaterialDesignMessageBox
{
    /// <summary>
    /// Create a message box popup that is formatted with the material design theme and that allows for theming of the buttons
    /// </summary>
    public static class MaterialMessageBox
    {
        private const MessageBoxButton _defaultButton = MessageBoxButton.OK;

         /// <summary>
        /// Displays a message box that has a message, a title bar caption, a specified button combination, a specified icon and a button color scheme; and that returns a result.
        /// </summary>
        /// <param name="owner">A System.Windows.Window that represents the owner of the MessageBox</param>
        /// <param name="messageBoxText">A System.String that specifies the text to display.</param>
        /// <param name="caption">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button">A System.Windows.MessageBoxButton value that specifies which button or buttons to display.</param>
        /// <param name="icon">A MaterialDesignThemes.Wpf.PackIconKind that specifies the icon to show to the left of the text.</param>
        /// <param name="stylePrimaryColor">A MaterialDesignColors.PrimaryColor that specifies the color scheme to use for the buttons.</param>
        /// <param name="primaryColorStyle">A ResourceDictionary referring to a MaterialDesignColors.PrimaryColor compliant style.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult ShowDialog(Window owner, string messageBoxText, string caption = null, MessageBoxButton? button = null, PackIconKind? icon = null, PrimaryColor? stylePrimaryColor = null, ResourceDictionary primaryColorStyle = null)
        {
            return ShowDialogCore(owner, messageBoxText, caption, button ?? _defaultButton, icon, stylePrimaryColor, primaryColorStyle);
        }
        
        /// <summary>
        /// Displays a message box that has a message, a title bar caption, a specified button combination, a specified icon and a button color scheme; and that returns a result.
        /// </summary>
        /// <param name="messageBoxText">A System.String that specifies the text to display.</param>
        /// <param name="owner">A System.Windows.Window that represents the owner of the MessageBox</param>
        /// <param name="caption">A System.String that specifies the title bar caption to display.</param>
        /// <param name="button">A System.Windows.MessageBoxButton value that specifies which button or buttons to display.</param>
        /// <param name="icon">A MaterialDesignThemes.Wpf.PackIconKind that specifies the icon to show to the left of the text.</param>
        /// <param name="stylePrimaryColor">A MaterialDesignColors.PrimaryColor that specifies the color scheme to use for the buttons.</param>
        /// <param name="primaryColorStyle">A ResourceDictionary referring to a MaterialDesignColors.PrimaryColor compliant style.</param>
        /// <returns>A System.Windows.MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult ShowDialog(string messageBoxText, Window owner = null, string caption = null, MessageBoxButton? button = null, PackIconKind? icon = null, PrimaryColor? stylePrimaryColor = null, ResourceDictionary primaryColorStyle = null)
        {
            return ShowDialogCore(owner, messageBoxText, caption, button ?? _defaultButton, icon, stylePrimaryColor, primaryColorStyle);
        }

        private static MessageBoxResult ShowDialogCore(Window owner, string messageBoxText, string caption, MessageBoxButton? button, PackIconKind? icon, PrimaryColor? stylePrimaryColor, ResourceDictionary primaryColorStyle)
        {
            // Setup new messagebox
            var messageBox = new MaterialDesignMessageBoxWindow(owner, messageBoxText, caption, button ?? _defaultButton, icon, stylePrimaryColor, primaryColorStyle);
            messageBox.ShowDialog();
            return messageBox.Result;
        }
    }
}
