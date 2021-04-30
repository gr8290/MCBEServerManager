using System.Collections.Generic;

namespace BEServerManager.Manager.ProcessLogManager
{
    abstract class ProcessLogManagerBase
    {
        private List<string> Log { get; set; } = new List<string>();

        public ProcessLogManagerBase()
        {
            
        }

        public void Init()
        {
            AddLogAction();
        }

        public void AddLog(string log)
        {
            Log.Add(log);
        }

        public List<string> GetLog()
        {
            return Log;
        }

        abstract protected void AddLogAction();

    }
}
