using Dapper.Contrib.Extensions;
using Setting.CustomTableAttribute;

namespace Setting.Sqlite.Models
{
    [TableName("sqlite_master")]
    [Table("sqlite_master")]
    public class SqliteMasterModel
    {
        [ColumnName("Type")]
        public string Type { get; set; }

        [ColumnName("Name")]
        public string Name { get; set; }

        [ColumnName("tbl_name")]
        public string tbl_name { get; set; }

        [ColumnName("RootPage")]
        public int RootPage { get; set; }

        [ColumnName("Sql")]
        public string Sql { get; set; }
    }
}
