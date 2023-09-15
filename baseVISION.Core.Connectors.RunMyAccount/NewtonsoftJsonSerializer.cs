using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers;

namespace baseVISION.Core.Connectors.RunMyAccount
{
    public class NewtonsoftJsonSerializer : IRestSerializer, ISerializer, IDeserializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }
        public NewtonsoftJsonSerializer()
        {
            this.serializer = new Newtonsoft.Json.JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,


            };
            this.serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

        }


        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public T Deserialize<T>(RestResponse response)
        {
            var content = response.Content;
            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                Newtonsoft.Json.JsonSerializer s = new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,


                };
                s.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                return new NewtonsoftJsonSerializer(s);
            }
        }


        public string Serialize(Parameter bodyParameter) => Serialize(bodyParameter.Value);



        public string[] AcceptedContentTypes { get; } = {
            "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
        };

        public ContentType ContentType { get; set; } = "application/json";

        public DataFormat DataFormat => DataFormat.Json;

        public ISerializer Serializer => this;

        public IDeserializer Deserializer => this;

        public SupportsContentType SupportsContentType => contentType => contentType.Value.Contains("json");


    }

    public class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
