using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    using Common;

    using Contracts;

    using Extension;

    using Services;

    [RoutePrefix("rest")]
    public class TestController : ApiController
    {
        private IEdfService edfService = new EdfService();

        [OperationAuthorize]
        [Route("organization/{theIdentifier}")]
        public DtoOrganization GetOrganization(string theIdentifier)
        {
            return edfService.GetOrganization(theIdentifier);
        }

        [HttpGet]
        [Route("test")]
        public string TestVersion()
        {
            return "version 1";
        }

        [HttpGet]
        public string TestVersion2()
        {
            return "version 2";
        }

        [ActionName("version3")]
        [HttpGet]
        public string TestException()
        {
            throw new WspException("test exception");
        }
    }
}