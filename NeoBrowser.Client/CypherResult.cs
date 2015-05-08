using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoBrowser.Client
{
    /// <summary>
    /// The result of a Cypher query.
    /// </summary>
    public class CypherResult
    {
        internal CypherResult(CypherStatement statement, string[] columns, IEnumerable<CypherResultRow> data)
        {
            Statement = statement;
            Columns = columns;
            Data = data;
        }


        public CypherStatement Statement { get; private set; }
        public string[] Columns { get; private set; }
        public IEnumerable<CypherResultRow> Data { get; private set; }
    }
}
