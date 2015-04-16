using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser
{
    public class GraphBrowser
    {
        private GraphClient _client;

        public GraphBrowser(string NeoUrl, string NeoUser, string NeoPassword)
        {
            // TODO: Complete member initialization
            Url = NeoUrl;
            User = NeoUser;
            Password = NeoPassword;
            Connect();
        }

        public string Url { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }


        private void Connect()
        {
            _client = new GraphClient(new Uri(Url));
            _client.Connect();
        }


        void browse()
        {
            var res = _client.Cypher.Match("", "").Return<object>("").Results;
            
        }

        public void Close()
        {
            // close connection to graph database
        }
    }
}
