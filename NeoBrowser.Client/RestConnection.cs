using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.Client
{
    class RestConnection
    {
        private readonly Uri _managementUri;
        private readonly Uri _dataUri;
        private ServiceRoot _serviceRoot;
        private string _username;
        private string _password;

        internal RestConnection(Uri graphEndpoint)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync(graphEndpoint.AbsoluteUri).Result.Content.ReadAsStringAsync().Result;
            var res = new { management = "", data = "" };
            res = JsonConvert.DeserializeAnonymousType(response, res);
            _managementUri = new Uri(res.management);
            _dataUri = new Uri(res.data);
        }

        private async Task<ServiceRoot> GetServiceRoot(HttpClient client)
        {
            _serviceRoot = _serviceRoot ?? await GetServiceRootImpl(client);
            return _serviceRoot;
        }

        private async Task<ServiceRoot> GetServiceRootImpl(HttpClient client)
        {
            var response = await client.GetAsync(_dataUri.AbsoluteUri);
            return await ReceiveJsonContent<ServiceRoot>(response);
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptCharset.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("UTF-8"));
            client.DefaultRequestHeaders.Add("X-stream", "true");
            if (_username != null)
            {
                var bytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _username, _password));
                string base64AuthenticationString = Convert.ToBase64String(bytes);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64AuthenticationString);
            }
            return client;
        }

        private HttpContent JsonContent(object o)
        {
            var content = new StringContent(JsonConvert.SerializeObject(o));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return content;
        }

        /// <summary>
        /// Creates an http stream content object based on the json serialization of the given .NET object.
        /// </summary>
        /// <param name="o">The object to serialize into the http stream</param>
        /// <returns>the http stream content</returns>
        private async Task<T> ReceiveJsonContent<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await ReceiveJsonContentImpl<T>(response);
            }
            else
            {
                JObject o = await ReceiveJsonContentImpl<JObject>(response);
                if (o != null)
                {
                    throw new GraphDatabaseException(o["errors"][0]["message"].ToString()) { StatusCode = o["errors"][0]["code"].ToString() };
                }
                else
                {
                    throw new GraphDatabaseException(response.StatusCode + " " + response.ReasonPhrase);
                }
            }
        }

        private static async Task<T> ReceiveJsonContentImpl<T>(HttpResponseMessage response)
        {
            string jsonContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        private IEnumerable<CypherResult> WrapCypherResults(JObject json, CypherStatement[] statements)
        {
            var exceptions = json["exceptions"];
            if (exceptions != null && json["exceptions"].Count() > 0)
            {
                throw new GraphDatabaseException(exceptions[0]["message"].ToString()) { StatusCode = exceptions[0]["message"].ToString() };
            }
            var results = json["results"];
            int i = 0;
            foreach (JObject res in results)
            {
                var rowdata = res["data"].Select(d => new CypherResultRow(d["row"]));
                var columns = res["columns"].Select(c => c.Value<string>()).ToArray();
                yield return new CypherResult(
                    statements[i++],
                    columns,
                    rowdata);
            }
        }

        internal void Authenticate(string username, string password)
        {
            _username = username;
            _password = password;
        }

        internal async Task<string> GetDatabaseVersion()
        {
            using (var client = CreateHttpClient())
            {
                var serviceRoot = await GetServiceRoot(client);
                return serviceRoot.neo4j_version;
            }
        }

        internal async Task<IEnumerable<CypherResult>> ExecuteCypherStatements(CypherStatement[] statements)
        {
            using (var client = CreateHttpClient())
            {
                var content = new { statements = statements.Select(stmt => stmt.PrepareToSend()) };
                var root = await GetServiceRoot(client);
                var response = await client.PostAsync(root.transaction.AbsoluteUri + "/commit", JsonContent(content));
                var json = await ReceiveJsonContent<JObject>(response);
                return WrapCypherResults(json, statements);
            }
        }

        internal async Task<List<string>> GetAllPropertyKeys()
        {
            return await Get<List<string>>(_dataUri.AbsoluteUri + "propertykeys");
        }

        internal async Task<Node> CreateNode(JObject properties)
        {
            using (var client = CreateHttpClient())
            {
                var root = await GetServiceRoot(client);
                var response = await client.PostAsync(root.node, JsonContent(properties));
                var node = await ReceiveJsonContent<Node>(response);
                node.Connection = this;
                return node;
            }
        }

        internal async Task<Node> GetNodeWithId(ulong id)
        {
            using (var client = CreateHttpClient())
            {
                var root = await GetServiceRoot(client);
                var url = string.Format("{0}/{1}", root.node.AbsoluteUri, id);
                var response = await client.GetAsync(url);
                var node = await ReceiveJsonContent<Node>(response);
                node.Connection = this;
                return node;
            }
        }

        internal async Task<T> Get<T>(string url)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(url);
                return await ReceiveJsonContent<T>(response);
            }
        }

        internal async Task Delete(string _objectUrl)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.DeleteAsync(_objectUrl);
                if (!response.IsSuccessStatusCode)
                {
                    await ReceiveJsonContent<Node>(response);
                }
            }
        }

        internal async Task<R> Post<R>(string uri, JObject obj)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.PostAsync(uri, JsonContent(obj));
                return await ReceiveJsonContent<R>(response);
            }
        }

        internal async Task Put(string url, object properties)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.PutAsync(url, JsonContent(properties));
                if (!response.IsSuccessStatusCode)
                {
                    await ReceiveJsonContent<Node>(response);
                }
            }
        }

        internal async Task<List<string>> GetAllRelationshipTypes()
        {
            return await Get<List<string>>(_dataUri.AbsoluteUri + "relationship/types");
        }

        internal async Task Post(string url, object properties)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.PutAsync(url, JsonContent(properties));
                if (!response.IsSuccessStatusCode)
                {
                    await ReceiveJsonContent<Node>(response);
                }
            }
        }
    }
}
