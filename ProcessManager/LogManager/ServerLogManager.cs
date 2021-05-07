namespace ProcessManager.LogManager
{
    public class ServerLogManager : LogManagerAbstract<ServerLogManager>
    {
        protected override void AddLogAction()
        {
            ProcessWrapper.Instance.DataReceivedActionList.Add(AddLog);
        }
    }
}
