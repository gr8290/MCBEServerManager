using BEServerManager.Data;
using ProcessManager;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace BEServerManager.Manager
{
    public class EntryUserManager
    {
        public delegate void ReceiveEntryUserEventHandler(List<EntryUserData> entryUserDictionary);
        public event ReceiveEntryUserEventHandler ReceiveEntryUserEvent;

        private List<EntryUserData> EntryUserDataList = new List<EntryUserData>();

        public EntryUserManager()
        {

        }

        public void Start()
        {
            ProcessWrapper.Instance.DataReceivedActionList.Add(ReceiveLog);
        }

        public void Stop()
        {
            ProcessWrapper.Instance.DataReceivedActionList.Remove(ReceiveLog);
        }

        private void Execute(string log)
        {
            if (log == null || log == "")
            {
                return;
            }
            Match connectedUserMatch = GetConnectedUserName(log);
            Match disconnectedUserMatch = GetDisconnectedUserName(log);
            Match XuidMatch = GetXuid(log);

            if (connectedUserMatch.Success)
            {
                AddUser(connectedUserMatch, XuidMatch);
            }
            else if (disconnectedUserMatch.Success)
            {
                RemoveUser(disconnectedUserMatch, XuidMatch);
            }

        }

        private void ReceiveLog(string log)
        {
            Execute(log);
            ReceiveEntryUserEvent(EntryUserDataList);
        }

        private Match GetDisconnectedUserName(string log)
        {
            string disconnectedUserReg = @"(?<=Player\sdisconnected:\s)[0-9a-zA-Z]*[0-9a-zA-Z]+?(?=,\sxuid:\s)";
            return Regex.Match(log, disconnectedUserReg);
        }

        private Match GetConnectedUserName(string log)
        {
            string connectedUserReg = @"(?<=Player\sconnected:\s)[0-9a-zA-Z]*[0-9a-zA-Z]+?(?=,\sxuid:\s)";
            return Regex.Match(log, connectedUserReg);
        }

        private Match GetXuid(string log)
        {
            string userXuid = @"(?<= (xuid:\s))\d{16}";
            return Regex.Match(log, userXuid);
        }

        private void AddUser(Match userName, Match xuid)
        {

            if (EntryUserDataList.Count == 0 || EntryUserDataList.Where(a => a.UserName.Equals(userName.Value) || a.Xuid.Equals(xuid.Value)).Count() == 0)
            {
                EntryUserData entryUserData = new EntryUserData
                {
                    UserName = userName.Value,
                    Xuid = xuid.Value
                };
                EntryUserDataList.Add(entryUserData);
            }
        }

        private void RemoveUser(Match userName, Match xuid)
        {
            if (EntryUserDataList.Count == 0 || EntryUserDataList.Where(a => a.UserName.Equals(userName.Value) || a.Xuid.Equals(xuid.Value)).Count() != 0)
            {
                EntryUserData entryUserData = new EntryUserData
                {
                    UserName = userName.Value,
                    Xuid = xuid.Value
                };
                EntryUserDataList.Remove(entryUserData);
            }
        }
    }
}
