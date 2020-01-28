using System.ComponentModel;
using System.Windows;

namespace Kernel.Workitems
{
    /// <summary>
    /// Abstraction over implementation framework's Window
    /// </summary>
    public interface IModalWindow
    {
        ResizeMode ResizeMode { get; set; }
        bool ShowIcon { get; set; }
        double Height { get; set; }
        double Width { get; set; }
        WindowStartupLocation WindowStartupLocation { get; set; }
        event CancelEventHandler Closing;
        string Title { get; set; }
        /// <summary>
        /// Show the window
        /// </summary>
        void Show();

        /// <summary>
        /// Show the window in dialog mode
        /// </summary>
        bool? ShowDialog();

        /// <summary>
        /// Bring the window into view
        /// </summary>
        void Focus();

        /// <summary>
        /// Take the window out of view
        /// </summary>
        void Unfocus();

        /// <summary>
        /// Close the window
        /// </summary>
        void Close();

        /// <summary>
        /// Get the underlying region holder
        /// </summary>
        /// <returns></returns>
        DependencyObject GetRegionHolder();

    }
}
