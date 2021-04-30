using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessManager
{
    public class ProcessWrapper
    {
        private Process _process = null;

        private bool isRunning = false;



        public bool IsRunning { get { return !(_process is null) && isRunning; } }

        public List<Action<string>> DataReceivedActionList = new List<Action<string>>();

        public List<Action<string>> DataSentActionList = new List<Action<string>>();

        public List<Action<bool>> ServerStatusChangedActionList = new List<Action<bool>>();

        public List<Action<bool>> ServerStartActionList = new List<Action<bool>>();

        public List<Action<bool>> ServerStopActionList = new List<Action<bool>>();

        private static ProcessWrapper instance { get; set; } = new ProcessWrapper();

        public static ProcessWrapper Instance
        {
            get
            {
                return instance;
            }
        }

        private ProcessWrapper()
        {
        }

        private void DataReceivedEvent(object sender, DataReceivedEventArgs e)
        {
            foreach (Action<string> a in DataReceivedActionList)
            {
                a(e.Data);
            }
        }

        private void DataSentActionEvent(string e)
        {
            foreach (Action<string> a in DataSentActionList)
            {
                a(e);
            }
        }

        private void StatusChangedActionEvent(bool e)
        {
            foreach (Action<bool> a in ServerStatusChangedActionList)
            {
                a(e);
            }
        }

        /// <summary>
        /// プロセスの開始
        /// </summary>
        public void Start(string filePath)
        {
            Process process = new Process();
            // BeginOutputReadLine()を利用するための条件
            process.StartInfo.UseShellExecute = false;

            // 標準入出力をリダイレクトするかどうか
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;

            //コンソール画面を開くかどうか
            process.StartInfo.CreateNoWindow = true;

            //パス指定
            process.StartInfo.FileName = filePath;
            process.OutputDataReceived += DataReceivedEvent;
            process.Exited += (object sender, EventArgs e) =>
            {
                //StopAsync();
            };

            _process = process;
            _process.Start();
            _process.BeginOutputReadLine();
            isRunning = true;
            StatusChangedActionEvent(isRunning);
        }

        public void Stop()
        {
            try
            {
                SendCommand("stop ");
                WaitForExitAsync(_process);
            }
            catch (InvalidOperationException ex1)
            {
                //MessageBoxUtil.ExceptionMessageBoxShow(ex1.ToString());
            }
            catch (Exception ex2)
            {
                //MessageBoxUtil.ExceptionMessageBoxShow(ex2.ToString());
            }
            finally
            {
                _process?.Close();
                _process?.Dispose();
                _process = null;
                isRunning = false;
                StatusChangedActionEvent(isRunning);
            }

        }

        public void Kill()
        {
            _process?.Kill();
        }

        public void SendCommand(string cmd, bool isDisplayOutput = true)
        {
            _process.StandardInput.WriteLine(cmd.Trim());
            if (isDisplayOutput) DataSentActionEvent(cmd.Trim());
        }

        private void WaitForExitAsync(Process process, CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<object>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.TrySetResult(null);
            if (cancellationToken != default(CancellationToken))
                cancellationToken.Register(tcs.SetCanceled);
        }
    }
}
