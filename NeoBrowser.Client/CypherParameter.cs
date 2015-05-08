using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoBrowser.Client
{
    public class CypherParameter
    {

        public CypherParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
    }
}
