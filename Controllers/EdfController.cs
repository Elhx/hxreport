using System.Web.Http;
using Contracts;
using Extension;

namespace WebApi.Controllers
{
    [RoutePrefix("rest")]
    [TaxonomyAndProductUniverseAuthorize]
    [WspContract("IRestService")]
    public class EdfController : ApiController
    {
        [Route("edf/{theIdentifier}")]
        public DtoOrganization GetEdfOrganization(string theIdentifier)
        {
            return new DtoOrganization { Identifiers = theIdentifier };
        }
    }
}