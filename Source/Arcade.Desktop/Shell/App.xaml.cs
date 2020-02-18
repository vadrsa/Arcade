using Arcade;
using Arcade.Configuration;
using Arcade.Views;
using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Prism;
using Infrastructure.Utility;
using Kernel;
using Kernel.Prism;
using Kernel.Workitems;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ArcadeWpfApplication
    {
        public App() : base(new ArcadeQueueProject())
        {
        }

    }
}
