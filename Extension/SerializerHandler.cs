using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    using System.IO;
    using System.Net;
    using System.Net.Http;

    using Contracts;

    public class SerializerHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.Content != null)
            {
                var result = ((ObjectContent)(response.Content)).Value;
                var aSerializeType = GetSerializeType(request);
                if (result.GetType() == typeof(DtoPlainText))
                {
                    return this.CreateResultMessage(response, result, aSerializeType);
                }
            }

            return response;
        }

        private string GetSerializeType(HttpRequestMessage request)
        {
            var query = request.RequestUri.ParseQueryString();
            return query["alt"];
        }

        private HttpResponseMessage CreateResultMessage(
            HttpResponseMessage response, object theResult, string theSerializeType)
        {
            var aResult = theResult as DtoPlainText;

            return response;
        }
    }
}
