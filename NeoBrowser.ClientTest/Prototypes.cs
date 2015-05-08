using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.ClientTest
{
    /// <summary>
    /// To fiddle with APIs, libraries, ...
    /// </summary>
    [TestFixture]
    public class Prototypes
    {
        [Test]
        public void JsonReaderPrototype()
        {
            JObject jo = new JObject();
            StringReader sr = new StringReader("{alpha: 1,\n beta:2}");
            JsonReader reader = new JsonTextReader(sr);
            Assert.True(reader.Read());
            Assert.AreEqual(reader.TokenType, JsonToken.StartObject);
            Assert.True(reader.Read());
            Assert.AreEqual(reader.TokenType, JsonToken.PropertyName);
            Assert.AreEqual(reader.Value, "alpha");
            Assert.True(reader.Read());
            Assert.AreEqual(reader.TokenType, JsonToken.Integer);
        }
    }
}
