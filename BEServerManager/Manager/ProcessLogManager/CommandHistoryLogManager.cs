using ProcessManager;

namespace BEServerManager.Manager.ProcessLogManager
{
    class CommandHistoryLogManager : ProcessLogManagerBase
    {
        private static readonly CommandHistoryLogManager instance = new CommandHistoryLogManager();

        public static CommandHistoryLogManager Instance
        {
            get
            {
                return instance;
            }
        }

        protected override void AddLogAction()
        {
            ProcessWrapper.Instance.DataSentActionList.Add(AddLog);
        }
    }
}
