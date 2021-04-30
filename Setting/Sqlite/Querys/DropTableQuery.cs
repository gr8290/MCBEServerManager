using Setting.Sqlite.Models;
using System.Collections.Generic;

namespace Setting.Sqlite.Querys
{
    class DropTableQuery
    {
        internal static readonly string DROP_TABLE_SEVER_QUERY = "drop table server;";

        internal static readonly string DROP_TABLE_WEB_API_QUERY = "drop table web_api";

        internal static Dictionary<string, string> GetAllQueryList()
        {
            return new Dictionary<string, string>
            {
                { TableInfo.GetTableName(typeof(ServerModel)), DROP_TABLE_SEVER_QUERY },
                { TableInfo.GetTableName(typeof(WebApiModel)), DROP_TABLE_WEB_API_QUERY }
            };
        }
    }
}
