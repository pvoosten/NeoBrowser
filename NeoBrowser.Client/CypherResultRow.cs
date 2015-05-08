using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoBrowser.Client
{
    public class CypherResultRow
    {
        private readonly JToken _rowData;

        public CypherResultRow(JToken rowData)
        {
            _rowData = rowData;
        }

        public JToken this[int index]
        {
            get
            {
                return _rowData[index];
            }
        }

        public T Get<T>(int index)
        {
            return _rowData[index].Value<T>();
        }
    }
}
