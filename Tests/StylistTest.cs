using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace BarksApp
{
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
            BDConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }
        public void Dispose()
        {
            Stylist.DeleteAll()
        }

        [Fact]
        public void Test_IfEmptyOnLoad_Empty()
        {
            
        }
    }
}
