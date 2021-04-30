using BEServerManager.Util;
using Microsoft.Win32;
using Setting.Sqlite;
using Setting.Sqlite.Models;
using System.IO;
using System.Windows;

namespace BEServerManager.View.InitialSetting
{
    /// <summary>
    /// InitialSettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class InitialSettingWindow : Window
    {
        public InitialSettingWindow()
        {
            InitializeComponent();
            MouseLeftButtonDown += (sender, e) => DragMove();
        }

        private void FileSelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "ファイルを選択",
                Filter = "exe Files (.exe)|*.exe|All Files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                FilePath.Text = dialog.FileName;
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputDataValid())
            {
                if (InsertInitData(FilePathName.Text, FilePath.Text))
                {
                    new MainWindow().Show();
                }
                else
                {
                    MessageBoxUtil.WarningMessageBoxShow("エラーが発生しました。終了します。");
                }
                CloseButton_Click(null, null);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool InsertInitData(string inputName, string inputFilePath)
        {
            using (SqliteWrapper sqlite = new SqliteWrapper(true))
            {
                try
                {
                    sqlite.ExecuteInsert(new ServerModel() { name = inputName, path = inputFilePath, target = 1 });
                    sqlite.Commit();
                }
                catch
                {
                    sqlite.RollBack();
                    return false;
                }
            }
            return true;
        }



        private bool InputDataValid()
        {
            if (string.IsNullOrEmpty(FilePathName.Text) || string.IsNullOrWhiteSpace(FilePathName.Text))
            {
                MessageBoxUtil.WarningMessageBoxShow($"名前が未入力または空白で構成されています。");
                return true;
            }
            if (string.IsNullOrEmpty(FilePath.Text) || string.IsNullOrWhiteSpace(FilePath.Text))
            {
                MessageBoxUtil.WarningMessageBoxShow($"ファイルパスが未入力または空白で構成されています。");
                return true;
            }
            if (!File.Exists(FilePath.Text))
            {
                MessageBoxUtil.WarningMessageBoxShow($"ファイルパスに入力されているファイルが存在しません。");
                return true;
            }
            return false;
        }

    }
}
