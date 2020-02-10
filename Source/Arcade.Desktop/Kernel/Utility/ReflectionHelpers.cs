using System;
using System.Linq;

namespace Kernel.Utility
{
    public static class ReflectionHelpers
    {
        public static Type GetGenericInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces().FirstOrDefault(x =>
              x.IsGenericType &&
              x.GetGenericTypeDefinition() == interfaceType);
        }
    }
}
