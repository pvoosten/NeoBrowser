using CypherNet.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            doMain().Wait();
            
        }

        static async Task doMain()
        {
            GraphStore store = new GraphStore("http://localhost:7474", "neo4j", "longbow");
            await store.InitializeAsync();
            var client = store.GetClient();
            var reader = client.QueryAsync("match (a) return a").Result;
            while(reader.Read())
            {
                Console.WriteLine("read line from reader");
                JObject obj = reader.Get<JObject>(0);
                Console.WriteLine(obj.ToString());

            }
        }

    }
}
