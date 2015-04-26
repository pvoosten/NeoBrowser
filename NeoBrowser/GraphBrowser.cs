using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CypherNet.Core;
using System.Threading.Tasks;

namespace NeoBrowser
{
    public class GraphBrowser
    {
        private GraphStore _store;
        private bool _initialized = false;

        public GraphBrowser(string host, int port, string user, string password, bool useSsl)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            UseSsl = useSsl;
            string url = string.Format("http{0}://{1}:{2}/", UseSsl ? "s" : "", Host, Port);
            _store = new GraphStore(url, User, Password);
        }

        public string Host { get; private set; }
        public int Port { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }
        public bool UseSsl { get; private set; }

        public int CurrentNodeId { get; private set; }

        private async Task<INeoClient> GetClient()
        {
            if (!_initialized)
            {
                await _store.InitializeAsync();
            }
            return _store.GetClient();
        }

        public async void GoToNode(int id)
        {
            var client = await GetClient();
            var reader = await client.QueryAsync(@"MATCH (a) RETURN Id(a)");
            while (reader.Read())
            {
                string s = reader.Get<string>(0);
                Console.WriteLine(s);
            }
        }
    }
}
