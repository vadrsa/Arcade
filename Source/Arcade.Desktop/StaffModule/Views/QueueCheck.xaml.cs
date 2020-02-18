using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StaffModule.Views
{
    /// <summary>
    /// Interaction logic for QueueCheck.xaml
    /// </summary>
    public partial class QueueCheck : UserControl
    {
        public QueueCheck()
        {
            InitializeComponent();
        }

        public void Print(Size size)
        {
            this.Measure(size);
            this.Arrange(new Rect(0, 0, size.Width, size.Height));

            this.UpdateLayout();
            var printDlg = new PrintDialog();

            printDlg.PrintVisual(this, "Queue ticket");
        }
    }
}
