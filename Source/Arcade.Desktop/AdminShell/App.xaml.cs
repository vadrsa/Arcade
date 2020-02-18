using Arcade;
using Kernel.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AdminShell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ArcadeWpfApplication
    {
        public App() : base(new ArcadeFullProject())
        {
        }
    }
}
