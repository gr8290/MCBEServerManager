using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setting.CustomTableAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    class TableNameAttribute : System.Attribute
    {
        internal string name;

        public TableNameAttribute(string name)
        {
            this.name = name;
        }
    }
}
