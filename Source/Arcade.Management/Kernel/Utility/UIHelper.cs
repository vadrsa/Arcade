using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Kernel.Utility
{
    public static class UIHelper
    {

        public static bool TryFocusWindow(Window window, bool activate = true)
        {
            try
            {
                window.Activate();
                window.Focus();
                window.Topmost = true;
                window.Topmost = false;
                IntPtr handle = new WindowInteropHelper(window).Handle;
                if (activate)
                    ActivateMainWindow(handle);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #region Window Focus Logic


        const int SW_RESTORE = 9;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static void ActivateMainWindow(IntPtr hWnd)
        {

            SetForegroundWindow(hWnd);

            // If program is minimized, restore it.
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SW_RESTORE);
            }
        }

        #endregion
    }
}
