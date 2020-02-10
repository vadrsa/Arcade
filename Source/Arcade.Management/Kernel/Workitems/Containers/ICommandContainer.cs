using System;
using System.Windows.Input;

namespace Kernel.Workitems
{
    /// <summary>
    /// Command container
    /// </summary>
    public interface ICommandContainer : IDisposable
    {
        /// <summary>
        /// Register a command with the container
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="command">The command to register</param>
        void Register(string name, ICommand command);
    }
}
