using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace NeoBrowser
{
    public class NodeLoadedEventArgs : EventArgs
    {
        public NodeLoadedEventArgs()
        {
            Node = null;

        }

        public JObject Node { get; internal set; }
        public IDictionary<string, List<JObject>> Connections { get; internal set; }
    }
}
