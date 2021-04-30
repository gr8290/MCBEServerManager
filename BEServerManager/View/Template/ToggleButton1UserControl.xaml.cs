using System.Windows;
using System.Windows.Controls;

namespace BEServerManager.View.Template
{
    /// <summary>
    /// Button1UserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ToggleButton1UserControl : UserControl
    {

        public static readonly DependencyProperty ButtonContentProperty =
    DependencyProperty.Register("ButtonContent", typeof(string), typeof(ToggleButton1UserControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ButtonTextProperty =
DependencyProperty.Register("ButtonText", typeof(string), typeof(ToggleButton1UserControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ButtonIsCheckedProperty =
DependencyProperty.Register("ButtonIsChecked", typeof(bool), typeof(ToggleButton1UserControl), new PropertyMetadata(false));

        public static readonly DependencyProperty ButtonVisibilityProperty =
DependencyProperty.Register("ButtonVisibility", typeof(Visibility), typeof(ToggleButton1UserControl), new PropertyMetadata(Visibility.Hidden));

        public static readonly RoutedEvent ButtonClickEvent =
    EventManager.RegisterRoutedEvent("ButtonClickEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleButton1UserControl));

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public bool ButtonIsChecked
        {
            get { return (bool)GetValue(ButtonIsCheckedProperty); }
            set { SetValue(ButtonIsCheckedProperty, value); }
        }

        public Visibility ButtonVisibility
        {
            get { return (Visibility)GetValue(ButtonVisibilityProperty); }
            set { SetValue(ButtonVisibilityProperty, value); }
        }

        public event RoutedEventHandler ButtonClickEventHandler
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }


        public ToggleButton1UserControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ButtonClickEvent));
        }

        public void MenuContentVisibility(Visibility visibility)
        {
            ContentName.Visibility = visibility;
        }
    }
}
