using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core
{
    public class LocalizationDictionary<K, V> : Dictionary<string, Dictionary<K,V>>
    {
        public bool HasCulture(string culture)
        {
            return this.ContainsKey(culture);
        }

        public Dictionary<K, V> GetCulture(string culture)
        {
            return this[culture];
        }
    }
}
