using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Views.Helpers
{
    /// <summary>
    /// Логика взаимодействия для LabelFieldView.xaml
    /// </summary>
    public partial class LabelFieldView : UserControl
    {
        public LabelFieldView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register
        (
            "PropertyValue",
            typeof(string),
            typeof(LabelFieldView),
            new PropertyMetadata(string.Empty)
        );
        
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register
        (
            "PropertyName",
            typeof(string),
            typeof(LabelFieldView),
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
