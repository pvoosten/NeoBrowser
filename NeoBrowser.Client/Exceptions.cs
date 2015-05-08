using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.Client
{
    /// <summary>
    /// The base class for all exceptions thrown in NeoBrowser.Client
    /// </summary>
    [Serializable]
    public class GraphDatabaseException : Exception
    {
        public GraphDatabaseException() { }
        public GraphDatabaseException(string message) : base(message) { }
        public GraphDatabaseException(string message, Exception inner) : base(message, inner) { }
        protected GraphDatabaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// The received status code. See http://neo4j.com/docs/2.2.0/status-codes.html for an explanation.
        /// </summary>
        public string StatusCode { get; internal set; }
    }
}
