using BEServerManager.Manager;
using BEServerManager.Manager.ProcessLogManager;
using ProcessManager;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BEServerManager.View.Home.BEServer
{
    /// <summary>
    /// BEServerLogUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class BEServerLogUserControl : UserControl
    {
        public BEServerLogUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            ProcessWrapper.Instance.DataReceivedActionList.Add(WriteTextBox);
            ProcessWrapper.Instance.ServerStatusChangedActionList.Add((serverStatus) =>
            {
                InputCmdGrid.Dispatcher.Invoke(() =>
                {
                    InputCmdGrid.IsEnabled = serverStatus;
                });
            });
        }

        private void WriteTextBox(string log)
        {

            ServetLogTextBox.Dispatcher.Invoke(() =>
            {
                if (!string.IsNullOrEmpty(log))
                {
                    ServetLogTextBox.AppendText($"{log}\r\n");
                    ServetLogTextBox.ScrollToEnd();
                }
            });
        }

        private void CmdSendBtn_Click(object sender, RoutedEventArgs e)
        {
            string cmdTrim = CmdTextBox.Text.Trim();
            string cmdToLower = cmdTrim.ToLower();
            if (cmdToLower.Equals("stop"))
            {
                ProcessWrapper.Instance.Stop();
            }
            else
            {
                ProcessWrapper.Instance.SendCommand(CmdTextBox.Text);
                ServetLogTextBox.AppendText($"{CmdTextBox.Text}\r\n");
                ServetLogTextBox.ScrollToEnd();
            }
            CmdTextBox.Clear();
        }

        private int _historyIndex = -1;

        private void CmdTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CmdSendBtn_Click(null, null);
                return;
            }
            TextBox textBox = (TextBox)sender;
            List<string> commandHistory = CommandHistoryLogManager.Instance.GetLog();
            if (e.Key == Key.Up)
            {
                if (_historyIndex < commandHistory.Count - 1)
                {
                    _historyIndex++;
                    textBox.Text = commandHistory[_historyIndex];
                }
                return;
            }

            if (e.Key == Key.Down)
            {
                if (_historyIndex > 0)
                {
                    _historyIndex--;
                    textBox.Text = commandHistory[_historyIndex];
                }
                return;
            }
        }
    }
}
