using Setting.Sqlite;
using Setting.Sqlite.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace BEServerManager.View.Setting.Parts
{
    /// <summary>
    /// WebApiSettingUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WebApiSettingUserControl : UserControl
    {
        private ObservableCollection<WebApiModel> WebApiSettingCollection { get; set; }

        public WebApiSettingUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            string baseAddres = String.Empty;
            using (SqliteWrapper sqliteWrapper = new SqliteWrapper(true))
            {
                try
                {

                    if (sqliteWrapper.ExecuteSelect<WebApiModel>().Count() == 0)
                    {
                        WebApiModel webApiModel = new WebApiModel()
                        {
                            base_addres = "http://localhost:1000/"
                        };
                        sqliteWrapper.ExecuteInsert(webApiModel);
                        sqliteWrapper.Commit();
                    }
                    baseAddres = sqliteWrapper.ExecuteSelect<WebApiModel>().FirstOrDefault().base_addres;
                }
                catch(Exception ex)
                {
                    sqliteWrapper.RollBack();
                }

            }
            BaseAddressTextBox.Text = baseAddres;
        }
    }
}
