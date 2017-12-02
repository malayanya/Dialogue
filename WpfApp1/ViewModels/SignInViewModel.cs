using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Shared.Models;
using WalletSimulator.Tools;
using WpfApp1.Properties;

namespace WpfApp1.ViewModels
{
    internal class SignInViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _password;
        private string _login;
        #endregion

        private readonly App _app = (App) Application.Current;

        #region Properties
        #region Command
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand SignInCommand { get; private set; }
        public RelayCommand SignUpCommand { get; private set; }
        #endregion

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConstructorAndInit
        internal SignInViewModel()
        {
            InitializeComands();
        }

        private void InitializeComands()
        {
            CloseCommand = new RelayCommand(obj => OnRequestClose(true));
            SignInCommand = new RelayCommand(SignIn,
                o => !String.IsNullOrEmpty(Login) &&
                     !String.IsNullOrEmpty(Password));
            SignUpCommand = new RelayCommand(SignUp);
        }
        #endregion

        private void SignUp(object obj)
        {
            OnRequestVisibilityChange(Visibility.Hidden);
            var signUpWindow = new Views.SignUpWindow();
            signUpWindow.ShowDialog();
            OnRequestVisibilityChange(Visibility.Visible);
        }

        private async void SignIn(object obj)
        {
            OnRequestLoader(true);
            var result = await Task.Run(() =>
            {
                User currentUser;
                try
                {
                    currentUser = _app.ChatService.GetUserByLogin(_login);
                }
                catch (Exception ex)
                {
                    Logger.Log(String.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine, ex.Message), ex);
                    MessageBox.Show(String.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
                        ex.Message));
                    return false;
                }
           
                if (currentUser == null)
                {
                    MessageBox.Show(String.Format(Resources.SignIn_UserDoesntExist, _login));
                    return false;
                }
                try
                {
                    if (!currentUser.CheckPassword(_password))
                    {
                        MessageBox.Show(Resources.SignIn_WrongPassword);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(
                        String.Format(Resources.SignIn_FailedToValidatePassword, Environment.NewLine, ex.Message), ex);
                    MessageBox.Show(String.Format(Resources.SignIn_FailedToValidatePassword, Environment.NewLine,
                        ex.Message));
                    return false;
                }
                _app.CurrentUser = currentUser;

                UserCredentials credentials = new UserCredentials(_login, _password);
                _app.SaveCredentialsForAutoLogin(credentials);
                return true;
            });
            OnRequestLoader(false);
            if (result)
                OnRequestClose(false);
        }

        #region EventsAndHandlers
        #region Close
        internal event CloseHandler RequestClose;
        internal delegate void CloseHandler(bool isQuitApp);

        internal virtual void OnRequestClose(bool isquitapp)
        {
            RequestClose?.Invoke(isquitapp);
        }
        #endregion

        #region ChangeVisibility
        internal event VisibilityHandler RequestVisibilityChange;
        internal delegate void VisibilityHandler(Visibility visibility);

        internal virtual void OnRequestVisibilityChange(Visibility visibility)
        {
            RequestVisibilityChange?.Invoke(visibility);
        }
        #endregion

        #region Loader
        internal event LoaderHandler RequestLoader;
        internal delegate void LoaderHandler(bool isShow);

        internal virtual void OnRequestLoader(bool isShow)
        {
            RequestLoader?.Invoke(isShow);
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion
    }
}
