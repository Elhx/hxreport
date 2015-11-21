using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    using System.IO;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;

    using ProtoBuf.Meta;

    public class ProtobufMediaTypeFormatter : MediaTypeFormatter
    {
        private static readonly Lazy<RuntimeTypeModel> ProtoBufModel = new Lazy<RuntimeTypeModel>(CreateProtoBufTypeModel);

        private static RuntimeTypeModel CreateProtoBufTypeModel()
        {
            var typeModel = TypeModel.Create();
            typeModel.UseImplicitZeroDefaults = false;

            return typeModel;
        }

        public ProtobufMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-protobuf"));
        }

        public ProtobufMediaTypeFormatter(MediaTypeMapping mediaTypeMapping) : this()
        {
            MediaTypeMappings.Add(mediaTypeMapping);
        }

        public ProtobufMediaTypeFormatter(IEnumerable<MediaTypeMapping> mediaTypeMappings)
            : this()
        {
            foreach (var mediaTypeMapping in mediaTypeMappings)
            {
                MediaTypeMappings.Add(mediaTypeMapping);
            }
        }

        public override bool CanReadType(Type type)
        {
            throw new NotImplementedException();
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return true;
        }

        public override Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext, System.Threading.CancellationToken cancellationToken)
        {
            ProtoBufModel.Value.Serialize(writeStream, value);
            return Task.FromResult(0);
        }
    }
}
