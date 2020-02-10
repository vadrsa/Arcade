using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mvvm
{
    public class FilteredObservableCollection<T> : ObservableCollection<T>
    {

        public FilteredObservableCollection() : base()
        {

        }

        public FilteredObservableCollection(List<T> list) : base(list)
        {

        }

        private List<T> removedList = new List<T>();

        public void Filter(Func<T, bool> predicate)
        {
            ClearFilter();
            foreach (var item in this.ToList())
            {
                if (!predicate.Invoke(item))
                {
                    removedList.Add(item);
                    this.Remove(item);
                }
            }
        }
        public void ClearFilter()
        {
            foreach (var item in removedList)
                this.Add(item);
            removedList = new List<T>();
        }
    }
}
