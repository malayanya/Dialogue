using System;
using System.IO;
using System.Text;

namespace WalletSimulator.Tools
{
    public static class Logger
    {
        private static readonly string Filepath = Path.Combine(StaticResources.ClientLogDirPath,
            DateTime.Now.ToString("dd.MM.yyyy") + ".txt");

        private static bool _firstCall = true;
        
        private static void CheckAndCreateFile()
        {
            if (!Directory.Exists(StaticResources.ClientLogDirPath))
            {
                Directory.CreateDirectory(StaticResources.ClientLogDirPath);
            }
            if (!File.Exists(Filepath))
            {
                File.Create(Filepath).Close();
            }

            if (_firstCall)
            {
                Console.WriteLine("Log file: " + Filepath);
                _firstCall = false;
            }
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);

            StreamWriter writer = null;
            FileStream file = null;
            try
            {
                CheckAndCreateFile();
                file = new FileStream(Filepath, FileMode.Append);
                writer = new StreamWriter(file);
                writer.WriteLine(DateTime.Now.ToString("HH:mm:ss.ms") + " " + message);
            }
            catch
            {
            }
            finally
            {
                writer?.Close();
                file?.Close();
                writer = null;
                file = null;
            }
        }
        public static void Log(string message, Exception ex)
        {
            Console.WriteLine(message);
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(message);
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Log(stringBuilder.ToString());
        }

        public static void Log(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);

            var stringBuilder = new StringBuilder();
            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            Log(stringBuilder.ToString());
        }
    }
}
