using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Storage
{
    public interface IStorage
    {
        IStorageResponse Request(string key);
        void Write(string key, object value);
    }
}
