using Setting.Sqlite.Models;
using System.Collections.Generic;

namespace Setting.Sqlite.Querys
{
    internal static class CreateTableQuery
    {
        internal static readonly string CREATE_TABLE_SEVER_QUERY = "create table server (id INTEGER PRIMARY KEY, name varchar(20), path text, target INTEGER not null);";

        internal static readonly string CREATE_TABLE_WEB_API_QUERY = "create table web_api (id INTEGER primary key not null, base_addres text);";

        internal static Dictionary<string, string> GetAllQueryList()
        {
            return new Dictionary<string, string>
            {
                { TableInfo.GetTableName(typeof(ServerModel)), CREATE_TABLE_SEVER_QUERY },
                { TableInfo.GetTableName(typeof(WebApiModel)), CREATE_TABLE_WEB_API_QUERY }
            };
        }
    }
}
