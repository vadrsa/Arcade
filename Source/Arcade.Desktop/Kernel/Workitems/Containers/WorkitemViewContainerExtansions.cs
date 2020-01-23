using System;

namespace Kernel.Workitems
{
    public static class WorkitemViewContainerExtansions
    {

        /// <summary>
        /// Register a veiw and return it
        /// </summary>
        /// <typeparam name="TView">Type of view</typeparam>
        /// <param name="viewContainer">The view container</param>
        /// <param name="view">The view</param>
        /// <param name="region">The region to add into</param>
        /// <returns></returns>
        public static TView Register<TView>(this IViewContainer viewContainer, TView view, string region)
        {
            return (TView)viewContainer.Register(view, region);
        }
    }
}
