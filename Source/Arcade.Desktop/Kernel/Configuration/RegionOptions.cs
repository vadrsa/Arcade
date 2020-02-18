using Kernel.Workitems;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Kernel.Configuration
{
    public class RegionOptions
    {

        public Func<Window> GetWindow { get; set; }

        public Func<IModalWindow> GetModalWindow { get; set; }

        public List<string> RegionNames { get; set; } = new List<string>();

        public bool IsSupported(string region)
        {
            return RegionNames.Contains(region);
        }

    }
}
