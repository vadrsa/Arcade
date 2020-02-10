using Infrastructure.Mvvm;
using Infrastructure.Workitems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectManagement
{
    public abstract class ObjectManagerDetailsViewModel<TDetails> : WorkitemViewModel<WorkitemWpfBase>
    {

        private TDetails _details;

        public TDetails Details
        {
            get { return _details; }
            set { Set(ref _details, value, nameof(Details)); }
        }

        public bool NeedReload { get; set; }

        private bool _isAdding;
        public bool IsAdding
        {
            get { return _isAdding; }
            set { Set(ref _isAdding, value, nameof(IsAdding)); }
        }
    }
}
