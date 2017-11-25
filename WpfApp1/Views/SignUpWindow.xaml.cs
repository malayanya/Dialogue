using System.Windows;
using FontAwesome.WPF;
using WpfApp1.ViewModels;
using WpfApp1.Views.Helpers;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    internal partial class SignUpWindow : Window
    {
        #region Fields
        private ImageAwesome _loader;
        #endregion

        #region Constructor
        internal SignUpWindow()
        {
            InitializeComponent();
            var signUpViewModel = new SignUpViewModel();
            signUpViewModel.RequestClose += Close;
            signUpViewModel.RequestLoader += OnRequestLoader;
            DataContext = signUpViewModel;
        }

        #endregion
        private void OnRequestLoader(bool isShow)
        {
            LoaderHelper.OnRequestLoader(MainGrid, ref _loader, isShow);
            IsEnabled = !isShow;
        }
    }
}
