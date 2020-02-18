using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Storage
{

    public interface IStorageResponse
    {
        bool Exists { get; }
        object Value { get; }

    }
    class StorageResponse : IStorageResponse
    {
        public bool Exists { get; set; }
        public object Value { get; set; }

    }
}
