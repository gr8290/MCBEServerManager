using BEServerManager.Manager.ProcessLogManager;
using BEServerManager.View.InitialSetting;
using Setting.Sqlite;
using Setting.Sqlite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BEServerManager
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        [STAThread()]
        static public void Main()
        {
            //ServerLogManager.Instance.Init();
            CommandHistoryLogManager.Instance.Init();
            SqliteEnvironment.Init();
            ProcessManager.LogManager.ServerLogManager.Instance.Init();

            var app = new App();
            app.InitializeComponent();
            app.Startup += App_Startup;
            app.Run();
        }
        private static void App_Startup(object sender, StartupEventArgs e)
        {
            //ファイルパスが設定されていない場合、初期設定画面を表示
            IEnumerable<ServerModel> res = null;
            using (SqliteWrapper sqlite = new SqliteWrapper())
            {
                res = sqlite.ExecuteSelect<ServerModel>();
            }
            if (res.Count() == 0 || res.FirstOrDefault(a => a.target == 1) == null)
            {
                new InitialSettingWindow().Show();
            }
            else
            {
                new MainWindow().Show();
            }
        }
    }
}
