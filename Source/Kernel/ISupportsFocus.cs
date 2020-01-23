namespace Kernel
{
    /// <summary>
    /// Adds focusability to implementing classes
    /// </summary>
    public interface ISupportsFocus
    {
        /// <summary>
        /// Is the object focused
        /// </summary>
        bool IsFocused { get; set; }

    }
}
