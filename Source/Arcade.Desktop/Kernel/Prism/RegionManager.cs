using Kernel.Prism;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Kernel
{
    public class RegionManager : IRegionManagerExtension
    {
        #region Static

        private static Dictionary<string, DependencyObject> regionTargets = new Dictionary<string, DependencyObject>();

        public static string GetRegionName(DependencyObject obj)
        {
            return (string)obj.GetValue(RegionNameProperty);
        }

        public static void SetRegionName(DependencyObject obj, string value)
        {
            obj.SetValue(RegionNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for RegionName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegionNameProperty =
            DependencyProperty.RegisterAttached("RegionName", typeof(string), typeof(RegionManager), new PropertyMetadata(null, RegionNameChanged));

        private static void RegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            regionTargets[(string)e.NewValue] = d;
            global::Prism.Regions.RegionManager.SetRegionName(d, (string)e.NewValue);
        }

        public static Type GetRegionType(string region)
        {
            return regionTargets[region].GetType();
        }
        #endregion

        IRegionManager _regionManager;
        IRegionTransformationCollection _transformationCollection;


        public RegionManager(IRegionManager regionManager, IRegionTransformationCollection transformationCollection)
        {
            this._regionManager = regionManager;
            this._transformationCollection = transformationCollection;
        }

        public IRegionCollection Regions => _regionManager.Regions;

        public object AddToRegion(string regionName, object view)
        {
            object newView = GetTransformed(regionName, view);
            _regionManager.AddToRegion(regionName, newView);
            return newView;
        }

        private object GetTransformed(string regionName, object view)
        {
            IRegionTransformation transformation = _transformationCollection.GetTransformation(Kernel.RegionManager.GetRegionType(regionName));
            if (transformation != null)
                view = transformation.Transform(view);
            return view;
        }
    }
}
