namespace ProcessManager.LogManager
{
    public class CommandHistoryLogManager : LogManagerAbstract<CommandHistoryLogManager>
    {
        protected override void AddLogAction()
        {
            ProcessWrapper.Instance.DataSentActionList.Add(AddLog);
        }
    }
}
