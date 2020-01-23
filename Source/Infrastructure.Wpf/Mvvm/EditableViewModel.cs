using Infrastructure.ChangeTracking;
using System.ComponentModel;
using System.Reflection;

namespace Infrastructure.Mvvm
{
    public abstract class EditableViewModel<T> : BaseViewModel, IEditableObject, IChangeTracking
        where T : class
    {
        public EditableViewModel()
        {
            PropertyChanged += EditableViewModel_PropertyChanged;
        }

        protected virtual bool PropertyFilter(PropertyInfo property) { return true; }

        private void EditableViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyInfo propertyInfo = this.GetType().GetProperty(e.PropertyName);
            if (propertyInfo != null)
            {
                if (PropertyFilter(propertyInfo))
                {
                    _isChanged = true;
                }
            }
        }

        private Memento<T> Memento;
        private bool _isChanged = false;

        public bool IsChanged => _isChanged;

        public void AcceptChanges()
        {
            _isChanged = false;
            EndEdit();
            BeginEdit();
        }

        public void BeginEdit()
        {
            Memento = new Memento<T>(this as T);
        }

        public void CancelEdit()
        {
            Memento?.Restore(this as T);
        }

        public void EndEdit()
        {
            Memento = null;
        }

    }
}
