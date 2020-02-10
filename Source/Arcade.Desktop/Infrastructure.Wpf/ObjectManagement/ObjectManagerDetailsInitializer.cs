using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectManagement
{
    public class ObjectManagerDetailsInitializer<T>
    {
        public bool IsAdding { get; set; } = true;
        public T Details { get; set; }
    }
}
