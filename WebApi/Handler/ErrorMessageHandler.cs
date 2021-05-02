using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Hosting;

namespace WebApi.Handler
{
    internal class ErrorMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                request.Properties.Remove(HttpPropertyKeys.NoRouteMatched);
                var errorResponse = request.CreateResponse(response.StatusCode, "コンテンツが見つかりません。");
                return errorResponse;
            }

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                request.Properties.Remove(HttpPropertyKeys.NoRouteMatched);
                var errorResponse = request.CreateResponse(response.StatusCode, "エラーが発生しました。");
                return errorResponse;
            }
            return response;
        }
    }
}