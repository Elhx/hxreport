namespace Services
{
    using Contracts;

    public class EdfService : IEdfService
    {
        public DtoOrganization GetOrganization(string theIdentifier)
        {
            return new DtoOrganization { Identifiers = theIdentifier };
        }
    }
}
