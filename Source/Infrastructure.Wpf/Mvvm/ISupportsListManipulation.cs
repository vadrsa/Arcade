using System.Threading.Tasks;

namespace Infrastructure.Mvvm
{
    public interface ISupportsListManipulation
    {
        Task RefreshItems(int? focuseID = null);
        void RemoveFromList(int id);
    }
}
