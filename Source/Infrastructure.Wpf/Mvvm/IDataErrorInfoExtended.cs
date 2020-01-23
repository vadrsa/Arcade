using System.ComponentModel;

namespace Infrastructure.Mvvm
{
    public interface IDataErrorInfoExtended : IDataErrorInfo
    {
        bool HasErrors { get; }
    }
}
