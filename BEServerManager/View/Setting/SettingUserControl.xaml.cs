using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BEServerManager.View.Setting
{
    /// <summary>
    /// SettingUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingUserControl : UserControl
    {
        private ResourceDictionary UserControlResources { get; set; }

        public SettingUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            UserControlResources = MainGrid.Resources;
            UserControl adduc = (UserControl)UserControlResources[BeServerManagerBtn.Tag.ToString()];
            adduc.SetValue(Grid.ColumnProperty, 1);
            MainGrid.Children.Add(adduc);
        }

        private void BeServerBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BeServerManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DeleteUserControlInGrid(btn);
            AddUserControlInGrid(btn);

        }

        private void WebApiBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DeleteUserControlInGrid(btn);
            AddUserControlInGrid(btn);
        }

        private void AdvancedSettingBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DeleteUserControlInGrid(btn);
            AddUserControlInGrid(btn);
        }

        private void DeleteUserControlInGrid(Button btn)
        {
            foreach (var i in UserControlResources.Keys)
            {
                if (i.ToString().Equals(btn.Tag.ToString()))
                {
                    continue;
                }
                UserControl remuc = (UserControl)UserControlResources[i.ToString()];
                if (MainGrid.Children.Contains(remuc))
                {
                    MainGrid.Children.Remove(remuc);
                    break;
                }
            }
        }

        private void AddUserControlInGrid(Button btn)
        {
            UserControl adduc = (UserControl)UserControlResources[btn.Tag.ToString()];
            if (!MainGrid.Children.Contains(adduc))
            {
                adduc.SetValue(Grid.ColumnProperty, 1);
                MainGrid.Children.Add(adduc);
            }
        }

    }
}
