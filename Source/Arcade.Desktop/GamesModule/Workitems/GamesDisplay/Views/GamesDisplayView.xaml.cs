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

namespace GamesModule.Workitems.GamesDisplay.Views
{
    /// <summary>
    /// Interaction logic for GamesDisplayView.xaml
    /// </summary>
    public partial class GamesDisplayView : UserControl
    {
        public GamesDisplayView()
        {
            InitializeComponent();
            LoadingIndicators.WPF.LoadingIndicator a = new LoadingIndicators.WPF.LoadingIndicator();
        }
    }
}
