using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebClient
{
    public class LoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //ObeidatLog.Helper.TryCatch( () =>
            //{
            //    ObeidatLog.Logger.Instance.Debug("Test", "Alaa", request);
            //});
            return  await base.SendAsync(request, cancellationToken);
        }
    }
}