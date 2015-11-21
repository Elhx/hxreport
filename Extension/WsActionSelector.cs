using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Extension
{
    public class WsActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            var aRequestedApiVersion = GetRequestedApiVersion(controllerContext);
            if (aRequestedApiVersion == null)
            {
                return base.SelectAction(controllerContext);
            }

            if (IsExceedTheMaxApiVersion(aRequestedApiVersion))
            {
                throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, "action not found"));
            }

            var aOriginalHttpActionDescriptor = base.SelectAction(controllerContext);
            var aGetNewActionDescriptor = GetNewActionDescriptor(aRequestedApiVersion, aOriginalHttpActionDescriptor);

            return aGetNewActionDescriptor;
        }
        
        private VersionNumber GetRequestedApiVersion(HttpControllerContext controllerContext)
        {
            var aRequestMessage = controllerContext.Request;
            var aRouteData = aRequestMessage.GetRouteData().GetSubRoutes().ToList();
            object aApiVersion;
            if (aRouteData.Count == 1 && aRouteData[0].Values.TryGetValue("apiVersion", out aApiVersion))
            {
                return new VersionNumber(aApiVersion.ToString().ToLower().Trim());
            }

            IEnumerable<string> aHeaderValues;
            if (aRequestMessage.Headers != null
                && aRequestMessage.Headers.TryGetValues("accept-version", out aHeaderValues)
                && aHeaderValues != null
                && aHeaderValues.Any())
            {
                return new VersionNumber(aHeaderValues.FirstOrDefault().ToLower().TrimStart('v').Trim());
            }

            return null;
        }

        private bool IsExceedTheMaxApiVersion(VersionNumber theRequestedApiVersion)
        {
            return theRequestedApiVersion > new VersionNumber("2.0.0");
        }

        private HttpActionDescriptor GetNewActionDescriptor(
            VersionNumber theRequestedApiVersion, 
            HttpActionDescriptor theOriginalHttpActionDescriptor)
        {
            var aApiVersionAttributes = theOriginalHttpActionDescriptor.GetCustomAttributes<ApiVersionAttribute>();
            if (aApiVersionAttributes == null || aApiVersionAttributes.Count == 0)
            {
                return theOriginalHttpActionDescriptor;
            }

            var aActionMappings = GetActionMapping(theOriginalHttpActionDescriptor.ControllerDescriptor);
            var aMatchedActionDescriptorMappings = new Dictionary<VersionNumber, HttpActionDescriptor>();
            foreach (var aApiVersionAttribute in aApiVersionAttributes)
            {
                var aActionName = aApiVersionAttribute.OperatioName;
                if (!aActionMappings.Contains(aActionName))
                {
                    continue;
                }

                var aHttpActionDescriptor = aActionMappings[aActionName].ElementAt(0);
                aMatchedActionDescriptorMappings[aApiVersionAttribute.VersionNumber] = aHttpActionDescriptor;
            }

            // this mapping could be cached
            if (aMatchedActionDescriptorMappings.Count == 0)
            {
                return theOriginalHttpActionDescriptor;
            }

            // customize version select logic here
            return CustomizedActionDescriptorSelector(theRequestedApiVersion, aMatchedActionDescriptorMappings);
        }

        private HttpActionDescriptor CustomizedActionDescriptorSelector(
            VersionNumber theRequestedApiVersion,
            IDictionary<VersionNumber, HttpActionDescriptor> theMatchedActionDescriptorMappings)
        {
            if (theRequestedApiVersion == new VersionNumber("1.0.0") || theRequestedApiVersion < new VersionNumber("1.0.0"))
            {
                return theMatchedActionDescriptorMappings[new VersionNumber("1.0.0")];
            }

            if (theRequestedApiVersion > new VersionNumber("1.0.0") &&
                theRequestedApiVersion < new VersionNumber("1.2.1"))
            {
                return theMatchedActionDescriptorMappings[new VersionNumber("1.0.0")];
            }

            if (theRequestedApiVersion < new VersionNumber("1.9.9"))
            {
                return theMatchedActionDescriptorMappings[new VersionNumber("1.2.1")];
            }

            return theMatchedActionDescriptorMappings[new VersionNumber("2.0.0")];
        }
    }
}
