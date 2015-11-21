using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Web.Http.Controllers;

    using Contracts;

    public class VersioningActionSelector : ApiControllerActionSelector
    {
        private static readonly string VERSION_QS = "version";

        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            string version = GetVersion(controllerContext);
            if (!string.IsNullOrEmpty(version))
            {
                controllerContext.RequestContext.RouteData.Values["action"] =
                    controllerContext.RequestContext.RouteData.Values["action"] + version;

                var aDes = base.SelectAction(controllerContext);
                return aDes;
            }

            return base.SelectAction(controllerContext);
        }

        public override ILookup<string, HttpActionDescriptor> GetActionMapping(HttpControllerDescriptor controllerDescriptor)
        {
            var aLookup = base.GetActionMapping(controllerDescriptor);
            return aLookup;
        }

        private static string GetVersion(HttpControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                return string.Empty;
            }

            var query = controllerContext.Request.RequestUri.ParseQueryString();
            return query[VERSION_QS];
        }
    }
}
