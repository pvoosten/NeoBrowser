using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.Client
{
    public abstract class PropertiesContainer
    {
        [JsonIgnore]
        internal RestConnection Connection { get; set; }

        [JsonProperty("properties")]
        private string _propertiesUri;
        public async Task<JObject> GetProperties()
        {
            return await Connection.Get<JObject>(_propertiesUri);
        }

        public async Task SetProperties(object properties)
        {
            await Connection.Put(_propertiesUri, properties);
        }

        [JsonProperty("property")]
        private string _propertyUri;

        public async Task<T> GetProperty<T>(string key)
        {
            return await Connection.Get<T>(_propertyUri.Replace("{key}", key));
        }

        public async Task SetProperty<T>(string key, T value)
        {
            await Connection.Put(_propertyUri.Replace("{key}", key), value);
        }

        public async Task DeleteProperty(string key)
        {
            await Connection.Delete(_propertyUri.Replace("{key}", key));
        }

        [JsonProperty("data")]
        public JObject Properties { get; private set; }

    }
}
