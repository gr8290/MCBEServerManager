using BEServerManager.Data;
using BEServerManager.Manager;
using BEServerManager.WebApi;
using ProcessManager;
using Setting.Sqlite;
using Setting.Sqlite.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BEServerManager.View.Home.BEServer
{
    /// <summary>
    /// BEServerControlMenuUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class BEServerControlMenuUserControl : UserControl
    {



        public BEServerControlMenuUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            ProcessWrapper.Instance.ServerStatusChangedActionList.Add(BeServerUserCtrl.ChangeStatus);
        }

        private void BeServerChangeStatus(object sender, RoutedEventArgs e)
        {

            if (ProcessWrapper.Instance.IsRunning == false)
            {
                using (SqliteWrapper sqlite = new SqliteWrapper())
                {
                    List<ServerModel> res = sqlite.ExecuteSelect<ServerModel>().ToList();
                    ProcessWrapper.Instance.Start(res.FirstOrDefault(a => a.target == 1).path);
                }


            }
            else if (ProcessWrapper.Instance.IsRunning == true)
            {
                ProcessWrapper.Instance.Stop();
            }
        }
    }
}
