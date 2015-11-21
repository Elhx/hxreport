using System;

namespace Extension
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WspContractAttribute : Attribute
    {
        private readonly string contractName;

        public WspContractAttribute(string theContractName)
        {
            contractName = theContractName;
        }

        public string ContractName 
        {
            get { return contractName; }
        }
    }
}