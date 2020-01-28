using Infrastructure.Mvvm;
using System.ComponentModel;

namespace Infrastructure.Utility
{
    public static class DataErrorInfoExtenisons
    {
        public static bool HasErrors(this IDataErrorInfo dataErrorInfo)
        {
            return IDataErrorInfoHelper.HasErrors(dataErrorInfo);
        }
    }
}
