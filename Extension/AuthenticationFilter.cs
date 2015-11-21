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
    using System.Web.Http.Filters;

    using DotNetOpenAuth.OAuth.ChannelElements;

    using Extension.Result;

    public class AuthenticationFilter : IAuthenticationFilter
    {
        private const string ACCESS_TOKEN_QS = "access_token";

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

        public bool AllowMultiple { get; private set; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
           
            string aToken = GetAccessTokenFromRequest(request);
            if (ValidateToken(aToken))
            {
                var aPrincipal = new OAuthPrincipal("testUser", null);
                context.Principal = aPrincipal;
            }
            else
            {
               // context.ErrorResult = new AuthenticationFailureResult("invalidate token", request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
