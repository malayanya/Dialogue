using System;
using System.IO;

namespace WalletSimulator.Tools
{
    public static class StaticResources
    {
        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string ClientDirPath = Path.Combine(AppData, "ChatProject");
        public static readonly string CurrentUserSerializedPath = Path.Combine(ClientDirPath, "CurrentUserCredentials.dat");
        public static readonly string ClientLogDirPath = Path.Combine(ClientDirPath, "Logs");
    }
}
