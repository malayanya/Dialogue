using System.Windows;
using FontAwesome.WPF;
using WalletSimulator.Tools;
using WpfApp1.ViewModels;
using WpfApp1.Views.Helpers;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    internal partial class SignInWindow : Window
    {
        #region Fields
        private ImageAwesome _loader;
        #endregion

        #region Constructor
        public SignInWindow()
        {
            InitializeComponent();
            var signInViewModel = new SignInViewModel();
            signInViewModel.RequestClose += CloseOrListChatsIfSignedIn;
            signInViewModel.RequestLoader += OnRequestLoader;
            signInViewModel.RequestVisibilityChange += (x) => Visibility = x;
            DataContext = signInViewModel;
        }
        #endregion

        private void OnRequestLoader(bool isShow)
        {
            LoaderHelper.OnRequestLoader(MainGrid, ref _loader, isShow);
            IsEnabled = !isShow;
        }

        #region EventHandlers
    
        private void CloseOrListChatsIfSignedIn(bool isQuitApp)
        {
            App app = ((App) Application.Current);

            if (!isQuitApp)
            {
                if (app.CurrentUser != null)
                {
                    var chatListWindow = new ChatListWindow();
                    chatListWindow.Show();
                    Close();
                    return;
                }
            }

            if (!isQuitApp)
                Close();
            else
                app.ShutDown(0);
        }
        #endregion
    }
}
