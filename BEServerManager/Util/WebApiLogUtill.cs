using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;
using System.Threading.Tasks;

namespace BEServerManager.Util
{
    class WebApiLogUtil : OwinMiddleware
    {
        public delegate void OutPutLogDelegate(IOwinContext con);
        public static OutPutLogDelegate OnOutPutLog;

        public WebApiLogUtil(
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
