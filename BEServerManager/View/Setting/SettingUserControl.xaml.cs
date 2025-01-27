﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            ToggleButton btn = (ToggleButton)sender;
            DeleteUserControlInGrid(btn);
            AddUserControlInGrid(btn);
            ActiveToggleButton(((ToggleButton)sender));
        }

        private void WebApiBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = (ToggleButton)sender;
            DeleteUserControlInGrid(btn);
            AddUserControlInGrid(btn);
            ActiveToggleButton(((ToggleButton)sender));
        }

        private void AdvancedSettingBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton btn = (ToggleButton)sender;
            DeleteUserControlInGrid(btn);
            AddUserControlInGrid(btn);
            ActiveToggleButton(((ToggleButton)sender));
        }

        private void DeleteUserControlInGrid(ToggleButton btn)
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

        private void AddUserControlInGrid(ToggleButton btn)
        {
            UserControl adduc = (UserControl)UserControlResources[btn.Tag.ToString()];
            if (!MainGrid.Children.Contains(adduc))
            {
                adduc.SetValue(Grid.ColumnProperty, 1);
                MainGrid.Children.Add(adduc);
            }
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

    }
}
