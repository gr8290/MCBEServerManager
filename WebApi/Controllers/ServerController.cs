using ProcessManager;
using Setting.Sqlite;
using Setting.Sqlite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ServerController : ApiController
    {
        [HttpGet]
        public JsonResult<BaseModel> Start()
        {
            BaseModel beServer = new BaseModel();
            if (ProcessWrapper.Instance.IsRunning == false)
            {
                using (SqliteWrapper sqlite = new SqliteWrapper())
                {
                    List<ServerModel> res = sqlite.ExecuteSelect<ServerModel>().ToList();
                    ProcessWrapper.Instance.Start(res.FirstOrDefault(a => a.target == 1).path);
                }
                beServer.Result = true;
            }
            else if (ProcessWrapper.Instance.IsRunning == true)
            {
                beServer.Result = false;
            }
            return Json(beServer);
        }

        [HttpGet]
        public JsonResult<BaseModel> Stop()
        {
            BaseModel beServer = new BaseModel();
            if (ProcessWrapper.Instance.IsRunning == false)
            {
                beServer.Result = false;
            }
            else if (ProcessWrapper.Instance.IsRunning == true)
            {
                ProcessWrapper.Instance.Stop();
                beServer.Result = true;
            }
            return Json(beServer);
        }

        [HttpGet]
        public JsonResult<BaseModel> Status()
        {
            BaseModel beServer = new BaseModel
            {
                Result = ProcessWrapper.Instance.IsRunning
            };
            return Json(beServer);
        }





        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
