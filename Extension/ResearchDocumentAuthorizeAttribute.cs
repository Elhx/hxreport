using System;
using System.Web.Http;

namespace Extension
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ResearchDocumentAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext == null
                || actionContext.RequestContext == null
                || actionContext.RequestContext.Principal == null)
            {
                return false;
            }

            if (!CheckResearchDocumentPermission(actionContext))
            {
                return false;
            }

            return base.IsAuthorized(actionContext);
        }

        private bool CheckResearchDocumentPermission(System.Web.Http.Controllers.HttpActionContext theActionContext)
        {
            var aUserPrincipal = theActionContext.RequestContext.Principal;
            object aDocId;
            theActionContext.RequestContext.RouteData.Values.TryGetValue("theDocId", out aDocId);
            return true;
        }
    }
}
