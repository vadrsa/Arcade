namespace Infrastructure.ChangeTracking
{
    /// <summary>
    /// Specifies the editing state of a unit
    /// </summary>
    public enum EditMode
    {
        /// <summary>
        /// The Default unediting state
        /// </summary>
        Default,
        /// <summary>
        /// Adding object state
        /// </summary>
        Add,
        /// <summary>
        /// Editing object state
        /// </summary>
        Edit
    }
}
