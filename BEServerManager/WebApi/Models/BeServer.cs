using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEServerManager.WebApi.Models
{
    public class BeServer
    {
        public bool IsRunning { get; set; }
        public string ErrorMessage { get; set; }
    }
}
