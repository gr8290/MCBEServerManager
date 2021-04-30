using Setting.Sqlite.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace Setting.Sqlite
{
    public static class SqliteEnvironment
    {

        public static void Init()
        {
            if (!IsExsistDb())
            {
                CreateDB();
            }

            using (SqliteWrapper sqliteWrapper = new SqliteWrapper())
            {
                foreach(var tn in NotExsistTableList())
                {
                    sqliteWrapper.CreateTable(tn);
                }
            }

        }

        /// <summary>
        /// DB作成
        /// </summary>
        private static void CreateDB()
        {
            SQLiteConnection.CreateFile("setting.sqlite");
        }

        /// <summary>
        /// DB存在確認
        /// </summary>
        /// <returns>DBが存在した場合はTrueを返却する。それ以外の場合はFalseを返却する。</returns>
        private static bool IsExsistDb()
        {
            return File.Exists($@"{Environment.CurrentDirectory}\setting.sqlite");
        }


        private static List<string> NotExsistTableList()
        {
            List<string> list = new List<string>();
            using (SqliteWrapper sqliteWrapper = new SqliteWrapper())
            {
                IEnumerable<SqliteMasterModel> sres = sqliteWrapper.ExecuteNotKeySelect<SqliteMasterModel>();
                foreach (var r in TableInfo.GetAllTableNameList())
                {
                    if (sres.Count(a =>  a.Name.Equals(r) && a.Type.Equals("table")) == 0  )
                    {
                        list.Add(r);
                    }
                }
            }
            return list;
        }

    }
}
