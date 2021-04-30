using ProcessManager.EntryUserManager.Data;
using ProcessManager.EntryUserManager.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProcessManager.EntryUserManager
{
    public class EntryUserManager
    {
        public delegate void ReceiveEntryUserEventHandler(List<UserData> UserDataList);
        public event ReceiveEntryUserEventHandler ReceiveEntryUserEvent;

        private List<UserData> EntryUserDataList = new List<UserData>();

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

        private void ReceiveLog(string log)
        {
            Execute(log);
            ReceiveEntryUserEvent(EntryUserDataList);
        }

        private void Execute(string log)
        {
            if (log == null || log == "")
            {
                return;
            }
            Match connectedUserMatch = GetUserInfoUtil.GetConnectedUserName(log);
            Match disconnectedUserMatch = GetUserInfoUtil.GetDisconnectedUserName(log);
            Match XuidMatch = GetUserInfoUtil.GetXuid(log);

            if (connectedUserMatch.Success)
            {
                AddUser(connectedUserMatch, XuidMatch);
            }
            else if (disconnectedUserMatch.Success)
            {
                RemoveUser(disconnectedUserMatch, XuidMatch);
            }
        }

        private void AddUser(Match userName, Match xuid)
        {
            if (EntryUserDataList.Count == 0 || EntryUserDataList.Where(a => a.UserName.Equals(userName.Value) || a.Xuid.Equals(xuid.Value)).Count() == 0)
            {
                UserData entryUserData = new UserData
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
                UserData entryUserData = new UserData
                {
                    UserName = userName.Value,
                    Xuid = xuid.Value
                };
                var target = EntryUserDataList.FirstOrDefault(a => a.UserName.Equals(userName.Value) && a.Xuid.Equals(xuid.Value));
                if (target != null)
                {
                    EntryUserDataList.Remove(target);
                }
            }
        }

    }
}
