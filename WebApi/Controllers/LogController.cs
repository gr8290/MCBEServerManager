using ProcessManager.LogManager;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class LogController: ApiController
    {
        [HttpGet]
        public JsonResult<LogModel> GetServerLog()
        {
            LogModel serverLogModel = new LogModel
            {
                LogList = ServerLogManager.Instance.GetLog(),
                Result = true
            };
            return Json(serverLogModel);
        }

        [HttpGet]
        public JsonResult<LogModel> GetCommandLog()
        {
            LogModel serverLogModel = new LogModel
            {
                LogList = CommandHistoryLogManager.Instance.GetLog(),
                Result = true
            };
            return Json(serverLogModel);
        }
    }
}
