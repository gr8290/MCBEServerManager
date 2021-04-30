using BEServerManager.Util;
using BEServerManager.WebApi;
using ProcessManager;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace BEServerManager
{

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly BeServerWebApi _webApi = new BeServerWebApi();
        private ResourceDictionary UserControlResources { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            UserControlResources = MainWindowGrid.Resources;
            AddUserControlInGrid("HomeMainUserControl", false);
            //AddHandler(ServerControlUserControl.SettingConfirmedEvent, new RoutedEventHandler(Window_UserControl_SettingConfirmedEventHandlerMethod));
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ProcessWrapper.Instance.IsRunning == true)
            {
                if (!MessageBoxUtil.QuestionMessageBoxShow("BEサーバが起動中です。終了しますか？\r\n\"はい\"を押すとBEサーバを停止して終了します。"))
                {
                    e.Cancel = true;
                    return;
                }
                ProcessWrapper.Instance.Stop();
            }
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((ToggleButton)sender).Tag.ToString(); ;
            DeleteUserControlInGrid(tag);
            AddUserControlInGrid(tag, true);
            ActiveToggleButton(((ToggleButton)sender));
        }

        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((ToggleButton)sender).Tag.ToString(); ;
            DeleteUserControlInGrid(tag);
            AddUserControlInGrid(tag, true);
            ActiveToggleButton(((ToggleButton)sender));
        }

        private void ActiveToggleButton(ToggleButton activeToggleButton)
        {
            if (activeToggleButton.IsChecked == false)
            {
                activeToggleButton.IsChecked = true;
                return;
            }

            foreach (var ch in MenuStackPanel.Children)
            {
                if (ch is ToggleButton toggleButton)
                {
                    if (toggleButton.Equals(activeToggleButton))
                    {
                        continue;
                    }
                    toggleButton.IsChecked = false;
                }
            }
        }

        private void DeleteUserControlInGrid(string resourcesKey)
        {
            foreach (var i in UserControlResources.Keys)
            {
                if (i.ToString().Equals(resourcesKey))
                {
                    continue;
                }
                UserControl remuc = (UserControl)UserControlResources[i.ToString()];
                if (MainWindowGrid.Children.Contains(remuc))
                {
                    MainWindowGrid.Children.Remove(remuc);
                    break;
                }
            }
        }

        private void AddUserControlInGrid(string resourcesKey, bool isCheckExistUserControl)
        {
            UserControl adduc = (UserControl)UserControlResources[resourcesKey];
            if (!isCheckExistUserControl || !MainWindowGrid.Children.Contains(adduc))
            {
                adduc.SetValue(Grid.ColumnProperty, 2);
                MainWindowGrid.RegisterName(adduc.Name, adduc);
                MainWindowGrid.Children.Add(adduc);
                Storyboard storyboard = new Storyboard();
                Storyboard.SetTarget((Storyboard)Resources["MenuButtonClickAnimation"], adduc);
                storyboard.Children.Add((Storyboard)Resources["MenuButtonClickAnimation"]);
                storyboard.Begin();
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            ((HwndSource)PresentationSource.FromVisual(this)).AddHook(WindowChromeUtil.HookProc);
        }

        private void MinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizedOrNormalButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                MaximizedOrNormalButton.Content = "1";
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaximizedOrNormalButton.Content = "2";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuCloseButton_Click(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            ColumnDefinition columnIndex0 = MainWindowGrid.ColumnDefinitions[0];
            columnIndex0.Width = new GridLength(20);
            MenuOpenButton.Visibility = Visibility.Visible;
        }

        private void MenuOpenButton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Visibility = Visibility.Collapsed;
            ColumnDefinition columnIndex0 = MainWindowGrid.ColumnDefinitions[0];
            columnIndex0.Width = new GridLength(175);
            MenuGrid.Visibility = Visibility.Visible;
        }
    }
}
