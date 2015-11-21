using System.Threading.Tasks;

namespace Extension
{
    using System.Net;
    using System.Net.Http;

    public class TrafficControlHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (ValidateTraffic())
            {
                return base.SendAsync(request, cancellationToken);
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }

        private bool ValidateTraffic()
        {
            return true;
        }
    }
}
