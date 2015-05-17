using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoBrowser.Client
{
    public class RelationshipMetadata
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }
    }
}
