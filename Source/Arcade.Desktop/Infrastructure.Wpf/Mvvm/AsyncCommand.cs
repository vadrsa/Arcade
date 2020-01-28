using AsyncAwaitBestPractices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Mvvm
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    public class AsyncCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        readonly Action<Exception> _onException;
        readonly bool _continueOnCapturedContext;

        public AsyncCommand(
            Func<Task> execute,
            Func<bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false)
        {
            _execute = execute;
            _canExecute = canExecute;
            _onException = onException;
            _continueOnCapturedContext = continueOnCapturedContext;
        }

        public bool IsExecuting => _isExecuting;

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    RaiseCanExecuteChanged();
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                    RaiseCanExecuteChanged();
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().SafeFireAndForget(_continueOnCapturedContext, _onException);
        }
        #endregion
    }

    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        public event EventHandler CanExecuteChanged;

        private Dictionary<T, bool> _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        readonly Action<Exception> _onException;
        readonly bool _continueOnCapturedContext;

        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false)
        {
            _execute = execute;
            _canExecute = canExecute;
            _onException = onException;
            _continueOnCapturedContext = continueOnCapturedContext;
            _isExecuting = new Dictionary<T, bool>();
        }

        public bool IsExecuting(T parameter)
        { 
            bool isExecuting = false;
            if (_isExecuting.ContainsKey(parameter))
                isExecuting = _isExecuting[parameter];
            return isExecuting;
        }

        public bool CanExecute(T parameter)
        {
            return !IsExecuting(parameter) && (_canExecute?.Invoke(parameter) ?? true);
        }

        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting[parameter] = true;
                    RaiseCanExecuteChanged();
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting[parameter] = false;
                    RaiseCanExecuteChanged();
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((T)parameter).SafeFireAndForget(_continueOnCapturedContext, _onException);
        }
        #endregion
    }
}
