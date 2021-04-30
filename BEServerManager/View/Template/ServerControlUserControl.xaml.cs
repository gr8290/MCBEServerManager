using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BEServerManager.View.Template
{
    /// <summary>
    /// ServerControlUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ServerControlUserControl : UserControl
    {

        public static readonly DependencyProperty ServerNameTextProperty =
            DependencyProperty.Register("ServerNameText", typeof(string), typeof(ServerControlUserControl), new PropertyMetadata(string.Empty));

        public static readonly RoutedEvent PowerButtonClickEvent =
            EventManager.RegisterRoutedEvent("PowerButtonClickEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ServerControlUserControl));

        public string ServerNameText
        {
            get { return (string)GetValue(ServerNameTextProperty); }
            set { SetValue(ServerNameTextProperty, value); }
        }


        public ServerControlUserControl()
        {
            InitializeComponent();
        }

        public void ChangeStatus(bool isRunnig)
        {
            StatusTextBlock.Dispatcher.Invoke(() =>
            {
                StatusTextBlock.Text = isRunnig ? "ステータス : 稼働中" : "ステータス : 停止中";
                PowerToggleButton.IsChecked = isRunnig;
            });
        }

        public event RoutedEventHandler PowerButtonClickEventHandler
        {
            add { AddHandler(PowerButtonClickEvent, value); }
            remove { RemoveHandler(PowerButtonClickEvent, value); }
        }

        private void PowerToggleButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PowerButtonClickEvent));
            ChangeStatus((bool)PowerToggleButton.IsChecked);
        }
    }
}
