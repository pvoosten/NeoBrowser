using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.Client
{
    public class Node : PropertiesContainer
    {
        [JsonConstructor]
        internal Node()
        {

        }

        [JsonProperty("labels")]
        private string _labelsUrl;

        public async Task<List<string>> GetLabels()
        {
            return await Connection.Get<List<string>>(_labelsUrl);
        }

        [JsonProperty("outgoing_relationships")]
        private string _outgoingRelationshipsUrl;
        [JsonProperty("all_typed_relationships")]
        private string _allTypedRelationshipsUrl;
        [JsonProperty("outgoing_typed_relationships")]
        private string _outgoingTypedRelationshipsUrl;
        [JsonProperty("incoming_relationships")]
        private string _incomingRelationshipsUrl;
        [JsonProperty("incoming_typed_relationships")]
        private string _incomingTypedRelationshipsUrl;
        [JsonProperty("all_relationships")]
        private string _allRelationshipsUrl;

        public async Task<List<Relationship>> GetRelationShips(Direction direction, params string[] types)
        {
            string url = null;
            if (types != null && types.Length > 0)
            {
                switch (direction)
                {
                    case Direction.Incoming:
                        url = _incomingTypedRelationshipsUrl;
                        break;
                    case Direction.Outgoing:
                        url = _outgoingTypedRelationshipsUrl;
                        break;
                    case Direction.Both:
                        url = _allTypedRelationshipsUrl;
                        break;
                    default:
                        throw new GraphDatabaseException("Unknown direction: " + direction);
                }
                url = url.Replace("{-list|&|types}", string.Join("&", types));
            }
            else
            {
                switch (direction)
                {
                    case Direction.Incoming:
                        url = _incomingRelationshipsUrl;
                        break;
                    case Direction.Outgoing:
                        url = _outgoingRelationshipsUrl;
                        break;
                    case Direction.Both:
                        url = _allRelationshipsUrl;
                        break;
                    default:
                        throw new GraphDatabaseException("Unknown direction: " + direction);
                }
            }
            var rels = await Connection.Get<List<Relationship>>(url);
            foreach (var rel in rels)
            {
                rel.Connection = Connection;
            }
            return rels;

        }

        [JsonProperty("self")]
        private string _selfUrl;

        public async Task<Node> GetUpdate()
        {
            return await Connection.Get<Node>(_selfUrl);
        }

        /// <summary>
        /// Deletes this node from the database.
        /// No further action is performed to synchronize managed objects.
        /// </summary>
        public async void Delete()
        {
            await Connection.Delete(_selfUrl);
        }

        [JsonProperty("create_relationship")]
        private string _createRelationshipUrl;
        public async Task<Relationship> CreateRelationShip(Node toNode, string relType, JObject properties = null)
        {
            var obj = JObject.FromObject(new
            {
                to = toNode._selfUrl,
                type = relType
            });
            if (properties != null)
            {
                obj.Add("data", properties);
            }
            return await Connection.Post<Relationship>(_createRelationshipUrl, obj);
        }

        [JsonProperty("extensions")]
        private JObject _extensions;
        [JsonProperty("traverse")]
        private string _traverseUrl;

        [JsonProperty("paged_traverse")]
        private string _pagedTraverseUrl;

        [JsonProperty("metadata")]
        public NodeMetadata Metadata { get; private set; }

        public async Task RemoveLabel(string label)
        {
            string deleteUrl = string.Format("{0}/{1}", _labelsUrl, label);
            await Connection.Delete(deleteUrl);
        }

        public async Task SetLabels(params string[] labels)
        {
            if (labels != null && labels.Length > 0)
            {
                await Connection.Post(_labelsUrl, labels);
            }
        }

        public async Task CreateRelationshipFrom(string relationshipType, ulong relatedNodeId)
        {
            Node node = await Connection.GetNodeWithId(relatedNodeId);
            await node.CreateRelationShip(this, relationshipType);
        }

        public async Task CreateRelationshipTo(string relationshipType, ulong relatedNodeId)
        {
            Node node = await Connection.GetNodeWithId(relatedNodeId);
            await CreateRelationShip(node, relationshipType);
        }
    }
}
