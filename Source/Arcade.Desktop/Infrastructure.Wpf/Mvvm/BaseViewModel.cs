using AutoMapper;
using GalaSoft.MvvmLight;
using Kernel.Managers;
using System;

namespace Infrastructure.Mvvm
{
    public class BaseViewModel : ViewModelBase, IDisposable
    {
        protected IUIManager UIManager { get; private set; }
        protected ITaskManager TaskManager { get; private set; }
        protected IMapper Mapper{ get; private set; }

        public BaseViewModel()
        {
            UIManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>();
            TaskManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();
            Mapper = CommonServiceLocator.ServiceLocator.Current.GetInstance<IMapper>();
        }


        public virtual void Dispose()
        {
        }
    }
}
