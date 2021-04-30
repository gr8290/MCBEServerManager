namespace ProcessManager.LogManager
{
    public class ServerLogManager : LogManagerAbstract<ServerLogManager>
    {
        protected override void AddLogAction()
        {
            ProcessWrapper.Instance.DataSentActionList.Add(AddLog);
        }
    }
}
