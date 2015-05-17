using NeoBrowser.Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.ClientTest
{
    [TestFixture]
    public class CreateGraphDatabaseTest
    {

        private bool testNodesCreated = false;
        private GraphDatabase gdb;

        [SetUp]
        public void SetUp()
        {
            testNodesCreated = false;
        }

        [TearDown]
        public async void TearDown()
        {
            if (testNodesCreated)
            {
               await gdb.ExecuteCypherStatements("MATCH (n:Testnode) DELETE n");
            }
        }

        [Test]
        public void GetServiceRoot()
        {
            gdb = new GraphDatabase(new Uri(TestUtil.URL));
        }

        [Test, ExpectedException(ExpectedException=typeof(GraphDatabaseException))]
        public async void GetDatabaseVersionWithoutAuthentication()
        {
            gdb = new GraphDatabase(TestUtil.URL);
            string version = await gdb.GetDatabaseVersion();
        }

        [Test]
        public async void GetDatabaseVersionWithAuthentication()
        {
            gdb = new GraphDatabase(TestUtil.URL)
                .Authenticate(TestUtil.USER, TestUtil.PASSWORD);
            string version = await gdb.GetDatabaseVersion();
            Assert.AreEqual("2.", version.Substring(0,2));
        }

        [Test]
        public async void SendParameterlessCyperhQueriesWithoutTransaction()
        {
            gdb = new GraphDatabase(TestUtil.URL)
                .Authenticate(TestUtil.USER, TestUtil.PASSWORD);
            var stmt = new CypherStatement(@"
                CREATE (n:Testnode {alpha: 1, beta:2})
                return n
            ");
            var res = await gdb.ExecuteCypherStatements(stmt);
            Assert.AreEqual(res.First().Columns.Length, 1);
        }

        [Test]
        public async void SendCyperhQueriesWithoutTransaction()
        {
            GraphDatabase gdb = new GraphDatabase(TestUtil.URL)
                .Authenticate(TestUtil.USER, TestUtil.PASSWORD);
            var stmt = new CypherStatement(@"
                CREATE (n:Testnode {alpha: {alpha}, beta:2})
                return n
            ", new CypherParameter("alpha", "alphaValue"));
            await gdb.ExecuteCypherStatements(stmt);
        }

        [Test]
        public async void GetAllPropertiesTest()
        {
            var keys = await TestUtil.GetGraphDb().GetAllPropertyKeys();
            Assert.Contains("name", keys);
        }

        [Test]
        public async void CreateNodeAndGetNodeWithId()
        {
            var gdb = TestUtil.GetGraphDb();
            var node = await gdb.CreateNode();
            var labels = await node.GetLabels();
            ulong id = node.Metadata.Id;
            var sameNode = await gdb.GetNodeWithId(id);
            Assert.AreEqual(node.Metadata.Labels, sameNode.Metadata.Labels);
        }

        [Test,ExpectedException(typeof(GraphDatabaseException))]
        public async void GetNonExistentNode()
        {
            var gdb = TestUtil.GetGraphDb();
            await gdb.GetNodeWithId(5555555);
        }



    }
}
