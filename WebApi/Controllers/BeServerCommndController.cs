using ProcessManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class BeServerCommndController : ApiController
    {
        [HttpPost]
        public JsonResult<BaseModel> Send(BeServerCommndModel command)
        {
            BaseModel bm = new BaseModel();
            if (ProcessWrapper.Instance.IsRunning)
            {
                ProcessWrapper.Instance.SendCommand(command.Command);
                bm.Result = true;
            }
            else
            {
                bm.Result = false;
            }
            return Json(bm);
        }
    }
}
