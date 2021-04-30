using BEServerManager.Util;
using Microsoft.Owin;
using System;
using System.Windows.Controls;

namespace BEServerManager.View.Home.WebApi
{
    /// <summary>
    /// WebApiLogUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WebApiLogUserControl : UserControl
    {
        public WebApiLogUserControl()
        {
            InitializeComponent();
        }

        private void AppendText(IOwinContext req)
        {
            HttpServetLogTextBox.Dispatcher.Invoke(() =>
            {
                HttpServetLogTextBox.AppendText($"[{DateTime.Now}] {req.Request.Uri}\n");
                HttpServetLogTextBox.ScrollToEnd();
            });
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            WebApiLogUtil.OnOutPutLog = AppendText;
        }
    }
}
