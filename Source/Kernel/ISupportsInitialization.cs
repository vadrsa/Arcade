namespace Kernel
{
    /// <summary>
    /// Adds initialization to a class
    /// </summary>
    public interface ISupportsInitialization
    {

        /// <summary>
        /// Initialize with data
        /// </summary>
        /// <param name="data">data to initialize with</param>
        void Initialize(object data);
    }
}
