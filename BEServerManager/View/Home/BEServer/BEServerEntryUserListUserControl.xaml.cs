using ProcessManager;
using ProcessManager.EntryUserManager;
using ProcessManager.EntryUserManager.Data;
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
        private ObservableCollection<UserData> UserNameCollection;
        private EntryUserManager entryUserManager;


        public BEServerEntryUserListUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            ProcessWrapper.Instance.ServerStatusChangedActionList.Add(ChangeServerStatus);
            test();
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
                UserNameCollection.Clear();
                SetCountEntryUser(0);
                Dispatcher.Invoke(() =>
                {
                });
            }
        }


        private void SetEntryUser(List<UserData> entryUserList)
        {
            UserNameCollection = new ObservableCollection<UserData>(entryUserList);
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
            List<UserData> entryUserDatas = new List<UserData>();
            for (int i = 0; i < 10; i++)
            {
                UserData entryUserData = new UserData();
                entryUserData.UserName = "test";
                entryUserData.Xuid = "aaaaaaaaaa";
                entryUserDatas.Add(entryUserData);
            }
            UserNameCollection = new ObservableCollection<UserData>(entryUserDatas);
            UserListBox.DataContext = UserNameCollection;
        }


    }
}
