using System;

namespace Kernel.Workitems.Behaviors
{
    public class WorkitemLaunchDescriptor
    {
        public WorkitemLaunchDescriptor(Type type, bool isRoot, bool isModal, IWorkItem parent)
        {
            _type = type;
            _isRoot = isRoot;
            _isModal = isModal;
            _parent = parent;
        }

        private IWorkItem _parent;
        private bool _isRoot;
        private bool _isModal;
        private Type _type;

        public IWorkItem Parent => _parent;
        public bool IsRoot => _isRoot;
        public bool IsModal => _isModal;
        public Type Type => _type;
    }
}
