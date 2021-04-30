using System;
using System.Collections.Generic;

namespace ProcessManager.LogManager
{
    public abstract class LogManagerAbstract<T> where T : class, new()
    {
        private List<string> Log { get; set; } = new List<string>();

        public static T Instance { get { return _sInstance.Value; } }

        private static readonly Lazy<T> _sInstance = new Lazy<T>(() => CreateInstanceOfT());

        public void AddLog(string log)
        {
            Log.Add(log);
        }

        public List<string> GetLog()
        {
            return Log;
        }

        abstract protected void AddLogAction();

        public void Init()
        {
            AddLogAction();
        }

        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
    }
}
