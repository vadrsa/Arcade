namespace Kernel.Workitems
{
    /// <summary>
    /// Object describing the view registered through the view container
    /// </summary>
    public class ViewDescriptor
    {
        public ViewDescriptor(object modified, object unmodified)
        {
            Modified = modified;
            Unmodified = unmodified;
        }

        /// <summary>
        /// The view passed to Register
        /// </summary>
        public object Modified { get; set; }

        /// <summary>
        /// The view actually registered
        /// </summary>
        public object Unmodified { get; set; }
    }
}
