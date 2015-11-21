using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Extension
{
    using System.Net.Http;

    using Contracts;

    public class DtoPermissionHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.Content != null)
            {
                //var result = ((ObjectContent)(response.Content)).Value;
                //var org = result as DtoOrganization;
                //if (org != null)
                //{
                //    org.Identifiers = "dto permission success";
                //}
                var objectContent = response.Content as ObjectContent;
                if (objectContent == null)
                {
                    return response;
                }

                var result = objectContent.Value;
                if (result == null)
                {
                    return response;
                }

                var resultType = result.GetType();
                var aIsGenericType = resultType.IsGenericType;
                if (aIsGenericType)
                {
                    resultType = resultType.GetGenericArguments()[0];
                }

                var aDtoPermissionFilter = DtoPermissionFilterFactory.GetFilterByDtoType(resultType);
                if (aDtoPermissionFilter == null)
                {
                    return response;
                }

                aDtoPermissionFilter.ApplyFilter(result, aIsGenericType);
            }

            return response;
        }
    }

    public class DtoPermissionFilterFactory
    {
        private static readonly IDictionary<Type, IDtoPermissionFilter> FilterDtoTypeMapping =
            new Dictionary<Type, IDtoPermissionFilter>
                {
                    { typeof(DtoDocument), new DtoDocumentPermissionFilter() },
                    { typeof(DtoOrganization), new DtoOrganizationPermissionFilter() }
                };

        public static IDtoPermissionFilter GetFilterByDtoType(Type theDtoType)
        {
            IDtoPermissionFilter aResult;
            FilterDtoTypeMapping.TryGetValue(theDtoType, out aResult);
            return aResult;
        }
    }

    public interface IDtoPermissionFilter
    {
        void ApplyFilter(object theDtoObject, bool theIsGenericCollection);
    }

    public class DtoDocumentPermissionFilter : IDtoPermissionFilter
    {
        public void ApplyFilter(object theDtoObject, bool theIsGenericCollection)
        {
            if (theIsGenericCollection)
            {
                var aDtoDocuments = theDtoObject as DtoCollection<DtoDocument>;
                if (aDtoDocuments != null)
                {
                    foreach (var aDto in aDtoDocuments.List)
                    {
                        aDto.Subscribe = true;
                    }
                }

                return;
            }

            var aDtoDocument = theDtoObject as DtoDocument;
            if (aDtoDocument != null)
            {
                aDtoDocument.Subscribe = true;
            }
        }
    }

    public class DtoOrganizationPermissionFilter : IDtoPermissionFilter
    {
        public void ApplyFilter(object theDtoObject, bool theIsGenericCollection)
        {
            if (theIsGenericCollection)
            {
                var aOrganizations = theDtoObject as DtoCollection<DtoOrganization>;
                return;
            }

            var aOrganization = theDtoObject as DtoOrganization;
        }
    }
}