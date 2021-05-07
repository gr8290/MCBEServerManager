using ProcessManager;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ServerCommndController : ApiController
    {
        [HttpPost]
        public JsonResult<BaseModel> Send(ServerCommndData command)
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
