using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WalletSimulator.Tools
{
    public static class SerializeManager
    {
        private static string CheckAndCreatePath(string filename)
        {
            if (!Directory.Exists(StaticResources.ClientDirPath))
                Directory.CreateDirectory(StaticResources.ClientDirPath);

            return Path.Combine(StaticResources.ClientDirPath, filename);
        }

        public static void Serialize<TObject>(TObject obj, string fileName)
        {
            var formatter = new BinaryFormatter();
            var filename = CheckAndCreatePath(fileName);

            using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }
        }
        public static TObject Deserialize<TObject>(string filename)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                return (TObject) formatter.Deserialize(fs);
            }
        }
    }
}
