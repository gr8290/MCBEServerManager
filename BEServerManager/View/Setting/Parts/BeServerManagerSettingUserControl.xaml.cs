using BEServerManager.Util;
using Microsoft.Win32;
using Setting.Sqlite;
using Setting.Sqlite.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BEServerManager.View.Setting.Parts
{
    /// <summary>
    /// BeServerManagerSettingUserControl.xaml の相互作用ロジック
    /// </summary>
    partial class BeServerManagerSettingUserControl : UserControl
    {
        ObservableCollection<ServerModel> FilePathSettingCollection { get; set; }

        private bool IsCombBoxSelectionChanged { get; set; } = false;

        public BeServerManagerSettingUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void ListBoxInDeleteBtn_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Button deleteCmd = (Button)sender;
            if (deleteCmd.DataContext is ServerModel deleteItem)
            {
                if (deleteItem.target == 1)
                {
                    MessageBoxUtil.WarningMessageBoxShow("起動対象のサーバとなっているので削除はできません。");
                    return;
                }

                using (SqliteWrapper sqliteWrapper = new SqliteWrapper(true))
                {
                    try
                    {
                        sqliteWrapper.ExecuteDelete(deleteItem);
                        sqliteWrapper.Commit();
                    }
                    catch
                    {
                        sqliteWrapper.RollBack();
                    }
                }
            }
            RefreshData();
        }

        private void AddSelectFilePathBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "ファイルを選択",
                Filter = "exe Files (.exe)|*.exe|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                AddFilePathTextBox.Text = dialog.FileName;
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Clear();
            AddFilePathTextBox.Clear();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBoxUtil.WarningMessageBoxShow($"{NameTextBlock.Text}が未入力または空白で構成されています。");
                return;
            }
            if (string.IsNullOrEmpty(AddFilePathTextBox.Text) || string.IsNullOrWhiteSpace(AddFilePathTextBox.Text))
            {
                MessageBoxUtil.WarningMessageBoxShow($"{AddSelectFilePathBtn.Content}が未入力または空白で構成されています。");
                return;
            }
            if (!File.Exists(AddFilePathTextBox.Text))
            {
                MessageBoxUtil.WarningMessageBoxShow($"ファイルが存在しません。");
                return;
            }
            if (FilePathSettingCollection.FirstOrDefault(a => a.name.Equals(NameTextBox.Text)) != null)
            {
                MessageBoxUtil.WarningMessageBoxShow($"名前が重複しています。");
                return;
            }

            ServerModel serverModelList = new ServerModel()
            {
                name = NameTextBox.Text,
                path = AddFilePathTextBox.Text
            };
            using (SqliteWrapper sqliteWrapper = new SqliteWrapper(true))
            {
                try
                {
                    sqliteWrapper.ExecuteInsert(serverModelList);
                    sqliteWrapper.Commit();
                }
                catch
                {
                    sqliteWrapper.RollBack(); ;
                }
            }
            RefreshData();
        }

        private void FilePathSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsCombBoxSelectionChanged)
            {
                ComboBox selectItem = (ComboBox)sender;
                if ((ServerModel)selectItem.SelectedItem == null)
                {
                    return;
                }
                using (SqliteWrapper sqliteWrapper = new SqliteWrapper(true))
                {
                    try
                    {
                        ServerModel nowTarget = sqliteWrapper.ExecuteSelect<ServerModel>().FirstOrDefault(a => a.target == 1);
                        nowTarget.target = 0;
                        sqliteWrapper.ExecuteUpdata(nowTarget);
                        ServerModel selectTarget = sqliteWrapper.ExecuteSelectOne<ServerModel>(((ServerModel)selectItem.SelectedItem).id);
                        selectTarget.target = 1;
                        sqliteWrapper.ExecuteUpdata(selectTarget);
                        sqliteWrapper.Commit();
                    }
                    catch
                    {
                        sqliteWrapper.RollBack(); ;
                    }
                }
            }
            IsCombBoxSelectionChanged = true;
        }

        private void RefreshData()
        {
            List<ServerModel> serverModelList = null;
            using (SqliteWrapper sqliteWrapper = new SqliteWrapper())
            {
                serverModelList = sqliteWrapper.ExecuteSelect<ServerModel>().ToList();
            }
            FilePathSettingCollection = new ObservableCollection<ServerModel>(serverModelList);
            FilePathSetting.DataContext = FilePathSettingCollection;
            FilePathSetting.Items.IndexOf(FilePathSettingCollection.Where(a => a.target == 1));
            ServerModel targetFile = FilePathSettingCollection.FirstOrDefault(a => a.target == 1);
            FilePathSetting.SelectedIndex = FilePathSettingCollection.IndexOf(targetFile);
            ServerListBox.DataContext = FilePathSettingCollection;
            SetTergetFilePathText(targetFile.name, targetFile.path);
        }

        private void SetTergetFilePathText(string name, string filePath)
        {
            TargetFilePathValueTextBlock.Text = name;
            TargetFilePathValueTextBox.Text = filePath;
        }


    }
}
