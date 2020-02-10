using System.Threading.Tasks;

namespace Infrastructure.Mvvm
{
    public interface ISupportsListManipulation
    {
        Task RefreshItems(object focuseID = null);
        void RemoveFromList(object id);
    }
}
