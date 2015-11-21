using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Extension
{
    public class WsControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration configuration;

        public WsControllerSelector(HttpConfiguration theConfiguration) : base(theConfiguration)
        {
            configuration = theConfiguration;
        }

        public override System.Web.Http.Controllers.HttpControllerDescriptor SelectController(System.Net.Http.HttpRequestMessage request)
        {
            string aRequestedApiVersion;

            var aOriginalRouteData = request.GetRouteData();
            if (aOriginalRouteData.Values.ContainsKey("api_version"))
            {
                aRequestedApiVersion = aOriginalRouteData.Values["api_version"].ToString();
                RedirectController(request, aRequestedApiVersion);
            }
            else
            {
                aRequestedApiVersion = GetRequestedApiVersionFromHttpHeader(request);
            }

            // set the requested version
            if (!string.IsNullOrWhiteSpace(aRequestedApiVersion))
            {
                request.Properties["Moodys_RequestedApiVersion"] = aRequestedApiVersion.ToLower().TrimStart('v').Trim();
            }

            var aResult = base.SelectController(request);
            return aResult;
        }

        private string GetRequestedApiVersionFromHttpHeader(HttpRequestMessage theRequest)
        {
            IEnumerable<string> aHeaderValues;
            if (theRequest.Headers != null 
                && theRequest.Headers.TryGetValues("accept-version", out aHeaderValues)
                && aHeaderValues != null)
            {
                return aHeaderValues.FirstOrDefault();
            }

            return null;
        }

        private void RedirectController(HttpRequestMessage theRequest, string theRequestedApiVersion)
        {
            // remove version segment, the goal is to matched to the default(no-version) route
            theRequest.Properties.Add("Moodys_OriginalUri", theRequest.RequestUri);
            theRequest.RequestUri = new Uri(theRequest.RequestUri.AbsoluteUri.Replace("/v" + theRequestedApiVersion + "/", "/"));

            // remove routingContext
            theRequest.Properties.Remove("MS_RoutingContext");

            // re-generate routeData
            var aNewRouteData = configuration.Routes.GetRouteData(theRequest);

            // set the new routeData to request
            theRequest.SetRouteData(aNewRouteData);
        }
    }
}