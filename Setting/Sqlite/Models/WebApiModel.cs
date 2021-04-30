using Dapper.Contrib.Extensions;
using Setting.CustomTableAttribute;

namespace Setting.Sqlite.Models
{
    [TableName("web_api")]
    [Table("web_api")]
    public class WebApiModel
    {
        [ColumnName("id")]
        [Key]
        public int id { get; set; }

        [ColumnName("base_address")]
        public string base_addres { get; set; }

    }
}
