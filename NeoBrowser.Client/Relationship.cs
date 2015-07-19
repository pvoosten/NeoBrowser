using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.Client
{
    public class Relationship : PropertiesContainer
    {

        [JsonConstructor]
        internal Relationship() { }


        [JsonProperty("extensions")]
        private JObject _extensions;

        /// <summary>
        /// Start node url
        /// </summary>
        [JsonProperty("start")]
        private string _startUri;

        public async Task<Node> GetStartNode()
        {
            return await GetNode(_startUri);
        }

        private async Task<Node> GetNode(string uri)
        {
            var node = await Connection.Get<Node>(uri);
            node.Connection = Connection;
            return node;
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
            return await GetNode(_endUri);
        }

        [JsonProperty("metadata")]
        public RelationshipMetadata Metadata { get; private set; }

    }
}
