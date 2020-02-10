namespace Kernel.Workitems
{
    public interface IWorkitemAware<T>
    {
        void SetWorkitem(T workItem);
    }
}
