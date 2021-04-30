using log4net;
using System.Runtime.CompilerServices;

namespace BEServerManager.Util
{
    static class Log4NetUtil
    {
        public static void InfoLogStart(string str, [CallerMemberName] string name = "")
        {
            LogManager.GetLogger(name).Info($"開始：{str}");
        }

        public static void InfoLogEnd(string str, [CallerMemberName] string name = "")
        {
            LogManager.GetLogger(name).Info($"開始：{str}");
        }


    }
}
