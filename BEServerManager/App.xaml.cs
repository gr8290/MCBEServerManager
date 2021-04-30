using BEServerManager.Manager.ProcessLogManager;
using ProcessManager;
using Setting.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BEServerManager
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServerLogManager.Instance.Init();
            CommandHistoryLogManager.Instance.Init();
            SqliteEnvironment.Init();
        }
    }
}
