using System;
using System.IO;
using System.Threading.Tasks;
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

            TryToAutoSignIn();

            Logger.Log("App started");
        }

        public void TryToAutoSignIn()
        {
            try
            {
                var userCredentials = SerializeManager.Deserialize<UserCredentials>(StaticResources.CurrentUserSerializedPath);
                User currentUser;

                try
                {
                    currentUser = ChatService.GetUserByLogin(userCredentials.Login);
                }
                catch (Exception ex)
                {
                    Logger.Log("Failed to get user by login", ex);
                    return;
                }

                if (currentUser == null)
                {
                    Logger.Log("User " + userCredentials.Login + "does not exist");
                    return;
                }
                try
                {
                    if (!currentUser.CheckPassword(userCredentials.Password))
                    {
                        Logger.Log("Wrong password " + userCredentials.Password);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(
                        String.Format(WpfApp1.Properties.Resources.SignIn_FailedToValidatePassword, Environment.NewLine, ex.Message), ex);
                    return;
                }
                CurrentUser = currentUser;
                Logger.Log("Auto signed in as " + userCredentials.Login);
            }
            catch (Exception e)
            {
                Logger.Log("Old user was not loaded", e);
            }
        }

        public void SaveCredentialsForAutoLogin(UserCredentials creds)
        {
            try
            {
                SerializeManager.Serialize<UserCredentials>(creds, StaticResources.CurrentUserSerializedPath);
                Logger.Log("Current user was serialized");
            }
            catch (Exception e)
            {
                Logger.Log("Current user was not saved", e);
            }
        }

        public void SignOut()
        {
            try
            {
                File.Delete(StaticResources.CurrentUserSerializedPath);
                CurrentUser = null;
            }
            catch (Exception e)
            {
            }
        }

        public void ShutDown(int exitCode)
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(exitCode);
        }
    }
}
