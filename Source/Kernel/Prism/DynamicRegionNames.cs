using Kernel.Configuration;
using System;
using System.Collections.Generic;

namespace Kernel.Prism
{
    /// <summary>
    /// Defines dynamic region names for every screen region
    /// </summary>
    public class DynamicRegionNames
    {
        // Dicitonary to keep track of names
        private Dictionary<Object, string> Names;

        public DynamicRegionNames(Project project)
        {
            // initialize Names dicitonary
            Names = new Dictionary<Object, string>();
            foreach(var id in project.GetOption<RegionOptions>().RegionNames)
                Names.Add(id, string.Format("{0}_{1}", id, Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// Get the dynamic name of a screen region
        /// </summary>
        /// <param name="forRegion">the screen region</param>
        /// <returns></returns>
        public string GetName(Object forRegion)
        {
            if (Names.ContainsKey(forRegion))
                return Names[forRegion];
            return null;
        }

    }
}
