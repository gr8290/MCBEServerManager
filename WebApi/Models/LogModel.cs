using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class LogModel : BaseModel
    {
        public List<string> LogList { get; set; }
    }
}
