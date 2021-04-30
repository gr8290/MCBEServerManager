using Setting.CustomTableAttribute;
using Dapper.Contrib.Extensions;

namespace Setting.Sqlite.Models
{
    [TableName("server")]
    [Table("server")]
    public class ServerModel
    {
        //[ColumnName("id")]
        [Key]
        public int id { get; set; }

        [ColumnName("name")]
        public string name { get; set; }

        [ColumnName("path")]
        public string path { get; set; }

        [ColumnName("target")]
        public int target { get; set; }

        //public bool IsTarget
        //{
        //    get
        //    {
        //        if (target == 0)
        //        {
        //            return false;
        //        }
        //        else if (target == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            throw new SystemException("設定が誤っています。");
        //        }
        //    }

        //}
    }
}
