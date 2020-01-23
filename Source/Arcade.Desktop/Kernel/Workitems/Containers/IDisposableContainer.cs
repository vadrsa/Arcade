using System;

namespace Kernel.Workitems
{
    /// <summary>
    /// Adds features that help a class manage its disposables better
    /// </summary>
    public interface IDisposableContainer : IDisposable
    {
        /// <summary>
        /// Registers an object as a disposable that will be disposed of 
        /// when the  Dispose method of the class is called
        /// </summary>
        T Disposable<T>(T obj) where T : IDisposable;
    }
}
