using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CypherNet.Core;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace NeoBrowser
{
    public class GraphBrowser
    {
        private GraphStore _store;
        private bool _initialized = false;

        private GraphBrowser()
        {

        }

        static GraphBrowser() {
            Instance = new GraphBrowser();
        }

        public static GraphBrowser Instance { get; private set; }

        public void Connect(string host, int port, string user, string password, bool useSsl)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            UseSsl = useSsl;
            string url = string.Format("http{0}://{1}:{2}/", UseSsl ? "s" : "", Host, Port);
            _store = new GraphStore(url, User, Password);
            FireConnected();
        }

        public event EventHandler Connected;

        private void FireConnected()
        {
            if (Connected != null)
            {
                Application.Current.Dispatcher.BeginInvoke(Connected, this, EventArgs.Empty);
            }
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

        public async Task<IEnumerable<string>> GetAllLabelsAsync(){
            var reader = await Query(@"
                MATCH (n) 
                WITH DISTINCT labels(n) as lbls 
                UNWIND lbls as lbl
                RETURN DISTINCT lbl 
                ORDER BY lbl");
            return GetAllLabelsFromReader(reader);
        }

        private static IEnumerable<string> GetAllLabelsFromReader(ICypherDataReader reader)
        {
            while (reader.Read())
            {
                yield return reader.Get<string>(0);
            }
        }

        private async System.Threading.Tasks.Task<ICypherDataReader> Query(string cypher)
        {
            string cypherString = string.Join(" ", Regex.Split(cypher.Trim(), "\\s+", RegexOptions.Singleline));
            var client = await GetClient();
            var reader = await client.QueryAsync(cypherString);
            return reader;
        }

        public async void GoToNode(int id)
        {
            var reader = await Query(@"MATCH (a) RETURN Id(a)");
            while (reader.Read())
            {
                string s = reader.Get<string>(0);
                Console.WriteLine(s);
            }
        }
    }
}
