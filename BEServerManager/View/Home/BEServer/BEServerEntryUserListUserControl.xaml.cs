using BEServerManager.Data;
using BEServerManager.Manager;
using ProcessManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BEServerManager.View.Home.BEServer
{
    /// <summary>
    /// BEServerEntryUserListUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class BEServerEntryUserListUserControl : UserControl
    {
        private ObservableCollection<EntryUserData> UserNameCollection;
        private EntryUserManager entryUserManager;


        public BEServerEntryUserListUserControl()
        {
            InitializeComponent();
        }

        private void ChangeServerStatus(bool flg)
        {
            if (flg)
            {
                entryUserManager = new EntryUserManager();
                entryUserManager.ReceiveEntryUserEvent += SetEntryUser;
                entryUserManager.Start();
            }
            else
            {
                entryUserManager.Stop();
                Dispatcher.Invoke(() =>
                {
                    UserNameCollection.Clear();
                    SetCountEntryUser(0);
                });
            }
        }


        private void SetEntryUser(List<EntryUserData> entryUserList)
        {
            UserNameCollection = new ObservableCollection<EntryUserData>(entryUserList);
            UserListBox.Dispatcher.Invoke(() =>
            {
                UserListBox.DataContext = UserNameCollection;
                SetCountEntryUser(UserNameCollection.Count);
            });
        }

        private void SetCountEntryUser(int n)
        {
            EntryUser.Text = $"{n.ToString()}/10";
        }

        private void test()
        {
            List<EntryUserData> entryUserDatas = new List<EntryUserData>();
            for (int i = 0; i < 10; i++)
            {
                EntryUserData entryUserData = new EntryUserData();
                entryUserData.UserName = "test";
                entryUserData.Xuid = "aaaaaaaaaa";
                entryUserDatas.Add(entryUserData);
            }
            UserNameCollection = new ObservableCollection<EntryUserData>(entryUserDatas);
            UserListBox.DataContext = UserNameCollection;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            ProcessWrapper.Instance.ServerStatusChangedActionList.Add(ChangeServerStatus);
            test();
        }
    }
}
