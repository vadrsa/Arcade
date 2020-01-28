using Arcade.Views;
using Kernel.Configuration;
using Kernel.Workitems;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Arcade
{
    class DialogHostWindow : IModalWindow
    {
        private DialogSession session;
        private Project project;
        private ModalRegionPopup popup;
        private ModalRegionPopup Popup
        {
            get
            {
                if (popup == null)
                    popup = new ModalRegionPopup(project);
                return popup;
            }
        }

        public DialogHostWindow(Project project)
        {
            this.project = project;
        }

        public ResizeMode ResizeMode { get; set; }
        public bool ShowIcon { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public WindowStartupLocation WindowStartupLocation { get; set; }
        public string Title { get; set; }

        public event CancelEventHandler Closing;

        public void Close()
        {
            session.Close();
        }

        public void Focus()
        {
        }

        public DependencyObject GetRegionHolder()
        {
            return Popup;
        }

        public void Show()
        {
            ShowDialog();
        }

        public bool? ShowDialog()
        {
            DialogHost.Show(Popup, OnDialogOpening, OnDialogClosing);
            return true;
        }

        private void OnDialogOpening(object sender, DialogOpenedEventArgs args)
        {
            session = args.Session;
        }

        private void OnDialogClosing(object sender, DialogClosingEventArgs args)
        {
            var cancel = new CancelEventArgs();
            Closing?.Invoke(this, cancel);
            if (cancel.Cancel)
            {
                args.Cancel();
                return;
            }
        }

        public void Unfocus()
        {
        }
    }
}
