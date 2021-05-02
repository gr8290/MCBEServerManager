using ProcessManager;
using Setting.Sqlite;
using Setting.Sqlite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class BeServerController : ApiController
    {
        [HttpGet]
        public JsonResult<BeServerModel> Start()
        {
            BeServerModel beServer = new BeServerModel();
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
        public JsonResult<BeServerModel> Stop()
        {
            BeServerModel beServer = new BeServerModel();
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
        public JsonResult<BeServerModel> Status()
        {
            BeServerModel beServer = new BeServerModel
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
