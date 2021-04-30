using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;
using System.Threading.Tasks;

namespace BEServerManager.Utill
{
    class WebApiLogUtill : OwinMiddleware
    {
        public delegate void OutPutLogDelegate(IOwinContext con);
        public static OutPutLogDelegate OnOutPutLog;

        public WebApiLogUtill(
            OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            OnOutPutLog(context);

            await Next.Invoke(context);
        }
    }
}
