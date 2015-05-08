using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NeoBrowser.Client
{
    public class CypherStatement
    {
        [JsonConstructor]
        private CypherStatement()
        {

        }

        public CypherStatement(string query, params CypherParameter[] queryParameters)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new GraphDatabaseException("Invalid query (null or white space)");
            }
            if (queryParameters.Select(p => p.Name).Distinct().Count() < queryParameters.Length)
            {
                throw new GraphDatabaseException("There are duplicate query parameter names");
            }
            Query = query;
            Parameters = queryParameters.ToList();
        }

        /// <summary>
        /// Implicitly coerce string to a cypher statement
        /// </summary>
        /// <param name="query">The query string to coerce</param>
        /// <returns>A parameterless query</returns>
        public static implicit operator CypherStatement(string query)
        {
            var stmt = new CypherStatement(query);
            return stmt;
        }

        public string Query { get; private set; }
        public IReadOnlyList<CypherParameter> Parameters { get; private set; }

        internal object PrepareToSend()
        {
            string formattedQuery = string.Join(" ", Regex.Split(Query.Trim(), @"\s+", RegexOptions.Singleline));
            if (Parameters.Count > 0)
            {
                return new
                {
                    statement = formattedQuery,
                    parameters = Parameters.ToDictionary(p => p.Name, p => p.Value)
                };
            }
            else
            {
                return new
                {
                    statement = formattedQuery
                };
            }
        }
    }
}
