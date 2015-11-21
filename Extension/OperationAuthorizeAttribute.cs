namespace Extension
{
    using System;
    using System.Security.Principal;
    using System.Web.Http;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class OperationAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext == null
                || actionContext.RequestContext == null
                || actionContext.RequestContext.Principal == null)
            {
                return false;
            }

            var aUserPrincipal = actionContext.RequestContext.Principal;
            var aContractName = actionContext.ActionDescriptor.ControllerDescriptor.GetContractName();
            var aOperationName = actionContext.ActionDescriptor.ActionName;
            if (!HasAccessToOperation(aUserPrincipal, aContractName, aOperationName))
            {
                return false;
            }

            //if (actionContext != null)
            //{
            //    var principal = Thread.CurrentPrincipal;
            //    if (principal == null)
            //    {
            //        return false;
            //    }
            //}
            return base.IsAuthorized(actionContext);
        }

        private bool HasAccessToOperation(IPrincipal theUserPrincipal, string theContractName, string theOperationName)
        {
            return true;
        }
    }
}
