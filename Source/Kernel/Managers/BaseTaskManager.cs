using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Kernel.Managers
{
    /// <summary>
    /// Handles anything to do with Tasks
    /// </summary>
    internal class BaseTaskManager : ITaskManager
    {
        #region Task Methods

        public async Task Run(Func<Task> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                // Try and run the task
                await Task.Run(function);
            }
            catch (Exception ex)
            {
                // TODO: Log error

                // Throw it as normal
                throw;
            }
        }


        public void RunNonUIThread(Action action, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {

            try
            {
                if (Application.Current.Dispatcher.CheckAccess())
                    Task.Run(action);
                else
                    action?.Invoke();
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }

        public void RunUIThread(Action action, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                // Invoke the action
                if (!Application.Current.Dispatcher.CheckAccess())
                {
                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        action();
                    }));

                }
                else action();
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }

        public async void RunAndForget(Func<Task> function, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                await Run(function, origin, filePath, lineNumber);
            }
            catch { }
        }

        public async Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                // Try and run the task
                return await Task.Run(function, cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }

        public async Task<TResult> Run<TResult>(Func<Task<TResult>> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                // Try and run the task
                return await Task.Run(function);
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }

        public async Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                // Try and run the task
                return await Task.Run(function, cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }

        public async Task<TResult> Run<TResult>(Func<TResult> function, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                // Try and run the task
                return await Task.Run(function);
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }

        public async Task Run(Func<Task> function, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                // Try and run the task
                await Task.Run(function, cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }

        public async void RunAndForget(Func<Task> function, CancellationToken cancellationToken, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                await Run(function, origin, filePath, lineNumber);
            }
            catch { }
        }

        public async Task Run(Action action, CancellationToken cancellationToken, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                // Try and run the task
                await Task.Run(action, cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }
        public async void RunAndForget(Action action, CancellationToken cancellationToken, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                await Run(action, origin, filePath, lineNumber);
            }
            catch { }
        }

        public async Task Run(Action action, [CallerMemberName]string origin = "", [CallerFilePath]string filePath = "", [CallerLineNumber]int lineNumber = 0)
        {
            try
            {
                // Try and run the task
                await Task.Run(action);
            }
            catch (Exception ex)
            {
                // TODO: Log error
                // Throw it as normal
                throw;
            }
        }

        public async void RunAndForget(Action action, [CallerMemberName] string origin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                await Run(action, origin, filePath, lineNumber);
            }
            catch { }
        }


        #endregion
    }
}