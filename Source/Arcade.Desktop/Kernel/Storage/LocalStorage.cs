using Kernel.Configuration;
using Kernel.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Storage
{
    class LocalStorage : IStorage
    {
        private string _identifier;
        private Project _project;

        public LocalStorage(string identifier, Project project) {
            _identifier = identifier;
            _project = project;
        }

        public string GetFileName()
        {
            var dirName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _project.AppDataFolderName, "LocalStorage");
            Directory.CreateDirectory(dirName);
            return Path.Combine(dirName, _identifier);
        }

        public IStorageResponse Request(string key)
        {
            var file = GetFileName();

            try
            {
                Dictionary<string, object> dict = null;
                if (File.Exists(file))
                {
                    dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(StringCipher.Decrypt(File.ReadAllText(file), _identifier));
                    if (!dict.ContainsKey(key))
                        return new StorageResponse { Exists = false };

                    return new StorageResponse { Exists = true, Value = dict[key] };
                }
                else
                {
                    return new StorageResponse { Exists = false };
                }
            }
            catch(Exception e)
            {

                if (File.Exists(file))
                    File.Delete(file);
                return new StorageResponse { Exists = false };
            }

        }

        public void Write(string key, object value)
        {
            var file = GetFileName();
            try
            {
                string content = null;
                Dictionary<string, object> dict = null;
                if (File.Exists(file))
                    content = StringCipher.Decrypt(File.ReadAllText(file), _identifier);
                if (content != null)
                    dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                else
                    dict = new Dictionary<string, object>();
                dict[key] = value;
                var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
                File.WriteAllText(file, StringCipher.Encrypt(serialized, _identifier));
            }
            catch(Exception e)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
        }

    }
}
