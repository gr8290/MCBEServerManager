using ProcessManager;

namespace BEServerManager.Manager.ProcessLogManager
{
    class ServerLogManager : ProcessLogManagerBase
    {
        private static readonly ServerLogManager instance = new ServerLogManager();

        public static ServerLogManager Instance
        {
            get
            {
                return instance;
            }
        }

        protected override void AddLogAction()
        {
            ProcessWrapper.Instance.DataReceivedActionList.Add(AddLog);
        }


    }
}
