using Setting.CustomTableAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Setting.Sqlite
{
    public static class TableInfo
    {
        public static List<string> GetAllTableNameList(bool IsSqliteMaster = false)
        {
            List<string> list = new List<string>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            assembly.GetTypes().Where(a => a.Namespace == "Setting.Sqlite.Models").ToList().ForEach(b => list.Add(GetTableName(b)));
            if (IsSqliteMaster == false)
            {
                list.Remove("sqlite_master");
            }
            return list;
        }

        public static string GetTableName(Type t)
        {
            return ((TableNameAttribute)Attribute.GetCustomAttributes(t, typeof(TableNameAttribute))[0]).name;
        }

        public static List<string> GetColumnsName<T>(bool IsHeadAtmark = false)
        {
            List<string> list = new List<string>();
            foreach (MemberInfo p in typeof(Models.ServerModel).GetProperties())
            {
                var pName = typeof(T).GetProperty(p.Name).GetCustomAttribute(typeof(ColumnNameAttribute));
                if (pName == null)
                {
                    continue;
                }
                string name;
                if (IsHeadAtmark)
                {
                    name = $"@{((ColumnNameAttribute)pName).name}";
                }
                else
                {
                    name = ((ColumnNameAttribute)pName).name;
                }
                list.Add(name);
            }
            return list;
        }

        public static Dictionary<string, List<string>> GetTableColumnPairs(Type t)
        {
            string tableName = ((TableNameAttribute)Attribute.GetCustomAttributes(t, typeof(TableNameAttribute))[0]).name;
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>() { { tableName, new List<string>() } };
            foreach (MemberInfo p in t.GetProperties())
            {
                var pName = t.GetProperty(p.Name).GetCustomAttribute(typeof(ColumnNameAttribute));
                if (pName == null)
                {
                    continue;
                }
                keyValuePairs[tableName].Add(((ColumnNameAttribute)pName).name);
            }
            return keyValuePairs;
        }

        public static Dictionary<string, object> GetColumns(Type t)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            foreach (MemberInfo p in t.GetProperties())
            {
                var pName = t.GetProperty(p.Name).GetCustomAttribute(typeof(ColumnNameAttribute));
                if (pName == null)
                {
                    continue;
                }
                keyValuePairs.Add(((ColumnNameAttribute)pName).name, null);
            }
            return keyValuePairs;
        }
    }
}
