using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.Client
{
    public class GraphDatabase
    {

        private RestConnection _connection;

        public GraphDatabase(Uri graphEndpoint): this(new RestConnection(graphEndpoint))
        {
        }

        public GraphDatabase(string graphEndpoint)
            : this(new Uri(graphEndpoint))
        {
        }

        internal GraphDatabase(RestConnection restConnection)
        {
            _connection = restConnection;
        }

        public async Task<string> GetDatabaseVersion()
        {
            return await _connection.GetDatabaseVersion();
        }


        public async Task<IEnumerable<CypherResult>> ExecuteCypherStatements(params CypherStatement[] statements)
        {
            if (statements == null || statements.Length == 0)
            {
                return Enumerable.Empty<CypherResult>();
            }
            return await _connection.ExecuteCypherStatements(statements);
        }

        /// <summary>
        /// Starts a new transaction
        /// </summary>
        /// <param name="forceCreation">if true, the transaction is started without statements being sent. Else, it is lazily created along with the first transaction statement.</param>
        /// <returns>The transaction</returns>
        public CypherTransaction BeginTransaction(bool forceCreation = false)
        {
            // the REST API supports sending statements at the start of a transaction.
            // Instead of using those semantics, we use SRP here and allow
            // sending statements only from within the transaction.
            // behind the scenes, the transaction is still only created when the first statement is sent.
            // That way, it is possible to fail on commit or rollbac, if no statements have been sent.
            return new CypherTransaction(_connection, forceCreation);

        }

        /// <summary>
        /// Adds or changes authentication to the API calls that follow.
        /// </summary>
        /// <param name="username">the username of the user that connects to the Neo4j database</param>
        /// <param name="password">the password of the user that connects to the Neo4j database</param>
        /// <returns>this, for chaining with the constructor</returns>
        public GraphDatabase Authenticate(string username, string password)
        {
            _connection.Authenticate(username, password);
            return this;
        }


        /// <summary>
        ///  list all property keys ever used in the database. This includes and property keys you have used, but deleted.
        ///  
        /// There is currently no way to tell which ones are in use and which ones are not, short of walking the entire set of properties in the database.
        /// </summary>
        /// <returns>all property keys used in the database</returns>
        public async Task<List<string>> GetAllPropertyKeys()
        {
            return await _connection.GetAllPropertyKeys();
        }

        public async Task<Node> CreateNode(JObject properties = null)
        {
            return await _connection.CreateNode(properties);
        }

        public async Task<Node> GetNodeWithId(ulong id)
        {
            return await _connection.GetNodeWithId(id);
        }


    }
}
