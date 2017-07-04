using Newtonsoft.Json;
using System.Collections.Generic;

namespace NeoBrowser.Client
{
    public class NodeMetadata
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("labels")]
        public List<string> Labels { get; set; }
    }
}