using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setting.CustomTableAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class ColumnNameAttribute : Attribute
    {
        internal string name;

        public ColumnNameAttribute(string name)
        {
            this.name = name;
        }
    }
}
