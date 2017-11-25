using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Views.Helpers
{
    /// <summary>
    /// Логика взаимодействия для LabelPasswordView.xaml
    /// </summary>
    public partial class LabelPasswordView : UserControl
    {
        public LabelPasswordView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register
        (
            "PropertyValue",
            typeof(string),
            typeof(LabelPasswordView),
            new PropertyMetadata(string.Empty)
        );

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register
        (
            "PropertyName",
            typeof(string),
            typeof(LabelPasswordView),
            new PropertyMetadata(string.Empty)
        );
        public string PropertyValue
        {
            get => (string)GetValue(PropertyValueProperty);
            set => SetValue(PropertyValueProperty, value);
        }

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }
    }
}
