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
    [WspContract("IRestService")]
    public class AtagController : ApiController
    {
        private IEdfService edfService = new EdfService();

        [Route("atag/{theIdentifier}")]
        public DtoOrganization GetOrganization(string theIdentifier)
        {
            return edfService.GetOrganization(theIdentifier);
        }

        [Route("atag/research")]
        public DtoCollection<DtoDocument> GetResearch()
        {
            return
                new DtoCollection<DtoDocument>(new List<DtoDocument>
                    {
                        new DtoDocument { DocId = "DOC_1" },
                        new DtoDocument { DocId = "DOC_2" }
                    });
        }

        [Route("atag/research/{theDocId}")]
        [ResearchDocumentAuthorize]
        public DtoDocument GetResearch(string theDocId)
        {
            return new DtoDocument { DocId = theDocId };
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("atagtest")]
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

        [ApiVersion("1.0.0", "GetRawResearchDocument")]
        [ApiVersion("1.2.1", "GetRawResearchDocumentV121")]
        [ApiVersion("2.0.0", "GetRawResearchDocumentV2")]
        [ApiVersion("v2.0.0-rc1", "GetRawResearchDocumentV2Rc1")]
        [Route("research/{theDocId}")]
        [Route("v{apiVersion}/research/{theDocId}")]
        public string GetRawResearchDocument(string theDocId)
        {
            return "version 1.0.0";
        }

        [Route("v1.2.1/research/{theDocId}")]
        public string GetRawResearchDocumentV121(string theDocId)
        {
            return "version 1.2.1";
        }

        [Route("v2.0.0/research/{theDocId}")]
        public string GetRawResearchDocumentV2(string theDocId)
        {
            return "version 2.0.0";
        }

        [Route("v2.0.0-rc1/research/{theDocId}")]
        public string GetRawResearchDocumentV2Rc1(string theDocId)
        {
            return "version 2.0.0-rc1";
        }
    }
}