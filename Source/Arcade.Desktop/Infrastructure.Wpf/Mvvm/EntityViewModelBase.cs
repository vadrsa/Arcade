using DevExpress.Mvvm;
using System;
using System.ComponentModel;

namespace Infrastructure.Mvvm
{
    public class EntityViewModelBase<T> : EditableViewModel<T>, IDataErrorInfoExtended
        where T : class, INotifyPropertyChanged
    {
        public EntityViewModelBase()
        {
        }

        string IDataErrorInfo.Error
        {
            get { return String.Empty; }
        }

        public bool HasErrors
        {
            get
            {
                return IDataErrorInfoHelper.HasErrors(this, true, 1, FilterValidationProperties);
            }
        }

        protected virtual bool FilterValidationProperties(PropertyDescriptor propertyDescriptor)
        {
            return propertyDescriptor.Name != nameof(HasErrors);
        }

        string IDataErrorInfo.this[string columnName]
        {
            get { return IDataErrorInfoHelper.GetErrorText(this, columnName); }
        }
    }
}
