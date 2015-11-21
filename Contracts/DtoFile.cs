namespace Contracts
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using ProtoBuf;

    [Serializable]
    [DataContract(Name = "document", Namespace = "http://api.moodys.com/REST")]
    [XmlRoot("document", Namespace = "http://api.moodys.com/REST", IsNullable = false)]
    [JsonObject(MemberSerialization.OptIn)]
    public class DtoFile
    {
        /// <summary>
        /// Without Exetension
        /// </summary>
        [DataMember(Name = "file_name", Order = 1)]
        [XmlElement(ElementName = "file_name", Order = 1)]
        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [DataMember(Name = "mime_type", Order = 2)]
        [XmlElement(ElementName = "mime_type", Order = 2)]
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [DataMember(Name = "size", Order = 3)]
        [XmlElement(ElementName = "size", Order = 3, IsNullable = true)]
        [JsonProperty("size")]
        public long? Size { get; set; }

        [DataMember(Name = "content", Order = 4)]
        [XmlElement(ElementName = "content", Order = 4)]
        [JsonProperty("content")]
        public string Base64Content { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [ProtoMember(5)]
        public bool NeedRightCheck { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [ProtoMember(6)]
        public bool NeedDisplayDirectly { get; set; }

        public DtoFile()
        {
            this.NeedDisplayDirectly = false;
        }
    }
}
