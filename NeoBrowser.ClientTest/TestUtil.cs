﻿using NeoBrowser.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBrowser.ClientTest
{
    internal static class TestUtil
    {
        public const string URL = "http://localhost:7474";
        public const string USER = "neo4j";
        public const string PASSWORD = "longbow";

        public static GraphDatabase GetGraphDb(bool authenticate = true)
        {
            var gdb = new GraphDatabase(TestUtil.URL);
            if(authenticate)
                gdb.Authenticate(TestUtil.USER, TestUtil.PASSWORD);
            return gdb;
        }

    }
}
