using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    using System.Net;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;

    using DotNetOpenAuth.OAuth.ChannelElements;

    public class AuthenticationHandler : DelegatingHandler
    {
        private const string ACCESS_TOKEN_QS = "access_token";

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            string aToken = GetAccessTokenFromRequest(request);
            if (ValidateToken(aToken))
            {
                var aPrincipal = new OAuthPrincipal("testUser", null);
                SetPrincipal(aPrincipal);

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

        private bool ValidateToken(string token)
        {
            return token != null;
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private static string GetAccessTokenFromRequest(HttpRequestMessage theHttpRequest)
        {
            if (theHttpRequest == null)
            {
                return string.Empty;
            }

            IEnumerable<string> aTokens;
            if (theHttpRequest.Headers.TryGetValues(ACCESS_TOKEN_QS, out aTokens))
            {
                return aTokens.FirstOrDefault();
            }

            var query = theHttpRequest.RequestUri.ParseQueryString();
            return query[ACCESS_TOKEN_QS];
        }
    }
}
