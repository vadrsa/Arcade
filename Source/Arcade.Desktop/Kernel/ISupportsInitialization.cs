namespace Kernel
{
    /// <summary>
    /// Adds initialization to a class
    /// </summary>
    public interface ISupportsInitialization<T>
    {

        /// <summary>
        /// Initialize with data
        /// </summary>
        /// <param name="data">data to initialize with</param>
        void Initialize(T data);
    }
}
