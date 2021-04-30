using BEServerManager.View.Template;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BEServerManager.View.MainMenu
{
    /// <summary>
    /// MainMenu.xaml の相互作用ロジック
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void OpenOrCloseButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = (ToggleButton)sender;
            toggleButton.Content = (bool)toggleButton.IsChecked ? "✖" : "三";
            foreach (var u in MainStackPane.Children)
            {
                if (u is ToggleButton1UserControl userControl)
                {
                    userControl.ButtonVisibility = (bool)toggleButton.IsChecked ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
