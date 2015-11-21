using System;

namespace Contracts
{
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    [Serializable]
    [DataContract(Name = "organization", Namespace = "http://www.moodys.com/api/ratings")]
    [XmlRoot("organization", Namespace = "http://api.moodys.com/REST", IsNullable = false)]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class DtoOrganization : DtoBaseObject
    {
        [DataMember(Name = "identifiers", Order = 2)]
        [XmlElement(ElementName = "identifiers", Order = 1, IsNullable = true)]
        [JsonProperty("identifiers")]
        public string Identifiers { get; set; }
    }
}
