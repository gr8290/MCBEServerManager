using BEServerManager.WebApi.Models;
using ProcessManager;
using Setting.Sqlite;
using Setting.Sqlite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace BEServerManager.WebApi.Controllers
{
    public class BeServerController : ApiController
    {

        [HttpGet]
        public JsonResult<BeServer> Start()
        {
            BeServer beServer = new BeServer();
            if (ProcessWrapper.Instance.IsRunning == false)
            {
                using (SqliteWrapper sqlite = new SqliteWrapper())
                {
                    List<ServerModel> res = sqlite.ExecuteSelect<ServerModel>().ToList();
                    ProcessWrapper.Instance.Start(res.FirstOrDefault(a => a.target == 1).path);
                }
                beServer.ErrorMessage = "ないよ( 一一)";
            }
            else if (ProcessWrapper.Instance.IsRunning == true)
            {
                beServer.ErrorMessage = "すでに稼働してます。(; ･`д･´)";
            }
            beServer.IsRunning = ProcessWrapper.Instance.IsRunning;
            return Json(beServer);
        }

        [HttpGet]
        public JsonResult<BeServer> Stop()
        {
            BeServer beServer = new BeServer();
            if (ProcessWrapper.Instance.IsRunning == false)
            {
                beServer.ErrorMessage = "稼働していないため、停止できませんでした。(; ･`д･´)";
            }
            else if (ProcessWrapper.Instance.IsRunning == true)
            {
                ProcessWrapper.Instance.Stop();
                beServer.ErrorMessage = "ないよ( 一一)";
            }
            beServer.IsRunning = ProcessWrapper.Instance.IsRunning;
            return Json(beServer);
        }


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
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
