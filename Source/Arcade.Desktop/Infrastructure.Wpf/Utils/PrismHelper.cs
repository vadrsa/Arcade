using Prism.Mvvm;
using System;
using System.Globalization;
using System.Reflection;

namespace Infrastructure.Utility
{
    public static class PrismHelper
    {
        public static void SetCustomViewTypeToViewModelTypeResolver()
        {

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                if (viewName.EndsWith("view", StringComparison.CurrentCultureIgnoreCase))
                {
                    viewName = viewName.Remove(viewName.Length - 4, 4);
                }
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                return Type.GetType(viewModelName);
            });
        }
    }
}
