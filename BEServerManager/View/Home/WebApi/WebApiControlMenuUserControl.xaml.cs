using BEServerManager.WebApi;
using System.Windows;
using System.Windows.Controls;

namespace BEServerManager.View.Home.WebApi
{
    /// <summary>
    /// WebApiControlMenuUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WebApiControlMenuUserControl : UserControl
    {

        public static readonly BeServerWebApi _webApi = MainWindow._webApi;

        public WebApiControlMenuUserControl()
        {
            InitializeComponent();
        }

        private void WebApiServerChangeStatus(object sender, RoutedEventArgs e)
        {
            if (_webApi == null || !_webApi.IsRunning)
            {
                _webApi.Start();
            }
            else if (_webApi.IsRunning)
            {
                _webApi.Stop();
            }
        }
    }
}
