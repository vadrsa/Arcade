using System.Windows;

namespace Kernel.Workitems
{
    public class ModalOptions
    {
        static Size DefaultSize = new Size(500, 300);
        static ResizeMode DefaultResizeMode = ResizeMode.CanResize;
        static WindowStartupLocation DefaultStartupLocation = WindowStartupLocation.CenterOwner;
        static bool DefaultShowIcon = false;
        static bool DefaultIsDialog = false;

        public ModalOptions()
        {
            Size = DefaultSize;
            ResizeMode = DefaultResizeMode;
            WindowStartupLocation = DefaultStartupLocation;
            ShowIcon = DefaultShowIcon;
            IsDialog = DefaultIsDialog;
        }

        public ModalOptions(Size size) : this()
        {
            Size = size;
        }

        public ModalOptions(Size size, ResizeMode resizeMode) : this(size)
        {
            ResizeMode = resizeMode;
        }

        public ModalOptions(Size size, ResizeMode resizeMode, WindowStartupLocation startupLocation) : this(size, resizeMode)
        {
            WindowStartupLocation = startupLocation;
        }

        public ModalOptions(Size size, ResizeMode resizeMode, WindowStartupLocation startupLocation, bool showIcon) : this(size, resizeMode, startupLocation)
        {
            ShowIcon = showIcon;
        }

        public ModalOptions(Size size, ResizeMode resizeMode, WindowStartupLocation startupLocation, bool showIcon, bool isDialog) : this(size, resizeMode, startupLocation, showIcon)
        {
            IsDialog = isDialog;
        }

        public WindowStartupLocation WindowStartupLocation { get; }

        public Size Size { get; }

        public bool ShowIcon { get; }

        public ResizeMode ResizeMode { get; }

        public bool IsDialog { get; }
    }
}
