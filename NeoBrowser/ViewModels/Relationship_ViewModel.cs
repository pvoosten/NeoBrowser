using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoBrowser.ViewModels
{
    public class Relationship_ViewModel
    {
        private Client.Relationship _rel;

        public Relationship_ViewModel(Client.Relationship rel)
        {
            _rel = rel;
            PropertiesJsonText = _rel.Properties.ToString(Formatting.Indented);
        }

        public string Type
        {
            get
            {
                return _rel.Type;
            }
        }

        public string PropertiesJsonText { get; private set; }
    }
}
