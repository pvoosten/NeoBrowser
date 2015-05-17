using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.Client
{
    public class Relationship
    {

        [JsonConstructor]
        internal Relationship() { }

        [JsonIgnore]
        internal RestConnection Connection { get; set; }

        [JsonProperty("extensions")]
        private JObject _extensions;

        /// <summary>
        /// Start node url
        /// </summary>
        [JsonProperty("start")]
        private string _startUri;

        public async Task<Node> GetStartNode()
        {
            return await Connection.Get<Node>(_startUri);
        }

        // http://localhost:7474/db/data/relationship/13/properties/{key}
        [JsonProperty("property")]
        private string _propertyUri;

        public async Task<T> GetProperty<T>(string key)
        {
            return await Connection.Get<T>(_propertyUri.Replace("{key}", key));
        }

        public async Task SetProperty<T>(string key, T value)
        {
            await Connection.Put(_propertyUri.Replace("{key}", key), value);
        }

        // http://localhost:7474/db/data/relationship/13
        [JsonProperty("self")]
        private string _selfUri;

        /// <summary>
        /// Delete this relationship in the database
        /// </summary>
        public async Task Delete()
        {
            await Connection.Delete(_selfUri);
        }

        // http://localhost:7474/db/data/relationship/13/properties
        [JsonProperty("properties")]
        private string _propertiesUri;
        public async Task<JObject> GetProperties()
        {
            return await Connection.Get<JObject>(_propertiesUri);
        }

        public async Task SetProperties(object properties)
        {
            await Connection.Put(_propertiesUri, properties);
        }

        /// <summary>
        /// The relationship type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; private set; }

        /// <summary>
        /// The end node url
        /// example: http://localhost:7474/db/data/node/31
        /// </summary>
        [JsonProperty("end")]
        private string _endUri;

        public async Task<Node> GetEndNode()
        {
            return await Connection.Get<Node>(_endUri);
        }

        [JsonProperty("metadata")]
        public RelationshipMetadata Metadata { get; private set; }

        [JsonProperty("data")]
        public JObject Properties { get; private set; }

    }
}
