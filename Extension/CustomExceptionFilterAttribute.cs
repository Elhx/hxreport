using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    using System.Net;
    using System.Web.Http.Filters;

    using Common;

    using log4net;

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly ILog logger = LogManager.GetLogger("WSLog");

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            if (actionExecutedContext.Exception is WspException)
            {
                logger.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
                actionExecutedContext.Response =
                    new System.Net.Http.HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }
        }
    }
}
