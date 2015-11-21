using System.Web.Http.Controllers;

namespace Extension
{
    public static class HttpControllerDescriptorExt
    {
        public static string GetContractName(this HttpControllerDescriptor theDescriptor)
        {
            var aWspContractAttributes = theDescriptor.GetCustomAttributes<WspContractAttribute>();
            if (aWspContractAttributes == null || aWspContractAttributes.Count == 0)
            {
                return string.Empty;
            }

            return aWspContractAttributes[0].ContractName;
        }
    }
}