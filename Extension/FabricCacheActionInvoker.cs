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

    public class FabricCacheActionInvoker : ApiControllerActionInvoker
    {
        public override Task<System.Net.Http.HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            if (actionContext.Request.Method == HttpMethod.Get)
            {
                var principal = Thread.CurrentPrincipal;
                if (principal == null)
                {
                    var controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
                    var actionName = actionContext.ActionDescriptor.ActionName;
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                                       {
                                           Content =
                                               new StringContent("tes")
                                       };
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
            }

            return base.InvokeActionAsync(actionContext, cancellationToken);
        }
    }
}
