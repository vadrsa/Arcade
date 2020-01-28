using System;

namespace Kernel.Workitems.Behaviors
{
    public class WorkitemLaunchDescriptor
    {
        public WorkitemLaunchDescriptor(Type type, bool isRoot, bool isModal)
        {
            _type = type;
            _isRoot = isRoot;
            _isModal = isModal;
        }

        private bool _isRoot;
        private bool _isModal;
        private Type _type;

        public bool IsRoot => _isRoot;
        public bool IsModal => _isModal;
        public Type Type => _type;
    }
}
