using System;

namespace Kernel.Workitems
{
    /// <summary>
    /// Make sure a workitem is opened only once,
    /// If a single instance workitem is open and another 
    /// is trying to open the current one will be focused instead
    /// </summary>
    public class SingleInstanceWorkitemAttribute : Attribute
    {
    }
}
