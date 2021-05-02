using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Util
{
    public class WebApiLogUtil: OwinMiddleware
    {
        public delegate void OutPutLogDelegate(IOwinContext con);
        public static OutPutLogDelegate OnOutPutLog;

        public WebApiLogUtil(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            OnOutPutLog(context);

            await Next.Invoke(context);
        }
    }
}
