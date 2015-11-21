using System;
using System.Web.Http;

namespace Extension
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class TaxonomyAndProductUniverseAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext == null
                || actionContext.RequestContext == null
                || actionContext.RequestContext.Principal == null)
            {
                return false;
            }

            if (!CheckTaxonomyPermission(actionContext))
            {
                return false;
            }

            return base.IsAuthorized(actionContext);
        }

        private bool CheckTaxonomyPermission(System.Web.Http.Controllers.HttpActionContext theActionContext)
        {
            var aUserPrincipal = theActionContext.RequestContext.Principal;
            var aActionName = theActionContext.ActionDescriptor.ActionName;
            object aEdfIdentifier;
            theActionContext.RequestContext.RouteData.Values.TryGetValue("theMirIdentifier", out aEdfIdentifier);
            return true;
        }
    }
}
