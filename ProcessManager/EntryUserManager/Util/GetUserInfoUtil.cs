using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessManager.EntryUserManager.Util
{
    public static class GetUserInfoUtil
    {
        public static Match GetDisconnectedUserName(string log)
        {
            string disconnectedUserReg = @"(?<=Player\sdisconnected:\s)[0-9a-zA-Z]*[0-9a-zA-Z]+?(?=,\sxuid:\s)";
            return Regex.Match(log, disconnectedUserReg);
        }

        public static Match GetConnectedUserName(string log)
        {
            string connectedUserReg = @"(?<=Player\sconnected:\s)[0-9a-zA-Z]*[0-9a-zA-Z]+?(?=,\sxuid:\s)";
            return Regex.Match(log, connectedUserReg);
        }

        public static Match GetXuid(string log)
        {
            string userXuid = @"(?<= (xuid:\s))\d{16}";
            return Regex.Match(log, userXuid);
        }
    }
}
