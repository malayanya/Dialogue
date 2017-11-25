using System;
using Shared;
using Shared.Models;
using System.Windows;
using WalletSimulator.Tools;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IChatService ChatService { get; }
        public User CurrentUser { get; set; }

        public App()
        {
            ChatService = new ChatServiceEmulator();
            // ChatService = new ChatServiceWrapper();

            Logger.Log("App started");
        }

        public void ShutDown(int exitCode)
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(exitCode);
        }
    }
}
