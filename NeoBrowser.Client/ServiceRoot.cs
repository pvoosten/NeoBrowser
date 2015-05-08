using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoBrowser.Client
{
    internal class ServiceRoot
    {
        public object extensions { get; set; }
        public Uri node { get; set; }
        public Uri node_index { get; set; }
        public Uri relationship_index { get; set; }
        public Uri extensions_info { get; set; }
        public Uri relationship_types { get; set; }
        public Uri batch { get; set; }
        public Uri cypher { get; set; }
        public Uri indexes { get; set; }
        public Uri constraints { get; set; }
        public Uri transaction { get; set; }
        public Uri node_labels { get; set; }
        public string neo4j_version { get; set; }
    }
}
